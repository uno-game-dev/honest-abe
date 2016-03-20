using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MobileInput : MonoBehaviour
{
    public enum Action { Null, LightAttack, HeavyAttack, Jump, Grab, Throw, Pickup }

    public float moveRadius = 50;
    public static float _horizontalAxisValue = 0;
    public static float _verticalAxisValue = 0;
    public static Action _lastAction = Action.Null;

    public Touch nullTouch;
    public Touch startMoveTouch;
    public Touch startActionTouch;

    private void Start()
    {
        nullTouch = new Touch();
        startMoveTouch = nullTouch;
        startActionTouch = nullTouch;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                if (isMoveTouch(touch))
                {
                    if (touch.phase == TouchPhase.Began)
                        SetMoveTouch(touch);
                    else if (touch.phase == TouchPhase.Moved)
                        SetMoveAxis(touch);
                    else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                        startMoveTouch = nullTouch;
                }
                else
                {
                    if (touch.phase == TouchPhase.Began)
                        SetActionTouch(touch);
                    else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                        SetAction(touch);
                }
            }
        }
        else
        {
            _horizontalAxisValue = Input.GetAxisRaw("Horizontal");
            _verticalAxisValue = Input.GetAxisRaw("Vertical");

        }
    }

    public static Action GetAction()
    {
        Action action = _lastAction;
        _lastAction = Action.Null;
        return action;
    }

    private void SetAction(Touch actionTouch)
    {
        Vector2 deltaPosition = actionTouch.position - startActionTouch.position;
        if (deltaPosition.magnitude < 10)
            _lastAction = Action.LightAttack;
        else if (SwipeLeft(deltaPosition))
            _lastAction = Action.Grab;
        else if (SwipeRight(deltaPosition))
            _lastAction = Action.Throw;
        else if (SwipeUp(deltaPosition))
            _lastAction = Action.Jump;
        else // if (SwipeDown(deltaPosition))
            _lastAction = Action.HeavyAttack;
    }

    private bool SwipeLeft(Vector2 deltaPosition)
    {
        if (Math.Abs(deltaPosition.x) >= Math.Abs(deltaPosition.y))
            if (deltaPosition.x < 0)
                return true;

        return false;
    }

    private bool SwipeRight(Vector2 deltaPosition)
    {
        if (Math.Abs(deltaPosition.x) >= Math.Abs(deltaPosition.y))
            if (deltaPosition.x > 0)
                return true;

        return false;
    }

    private bool SwipeUp(Vector2 deltaPosition)
    {
        if (Math.Abs(deltaPosition.y) >= Math.Abs(deltaPosition.x))
            if (deltaPosition.y < 0)
                return true;

        return false;
    }

    private void SetMoveAxis(Touch moveTouch)
    {
        _horizontalAxisValue = moveTouch.position.x;
        _horizontalAxisValue -= startMoveTouch.position.x;
        _horizontalAxisValue /= moveRadius;
        _horizontalAxisValue = Mathf.Clamp01(_horizontalAxisValue);

        _verticalAxisValue = moveTouch.position.y;
        _verticalAxisValue -= startMoveTouch.position.y;
        _verticalAxisValue /= moveRadius;
        _verticalAxisValue = Mathf.Clamp01(_verticalAxisValue);
    }

    private void SetActionTouch(Touch touch)
    {
        if (!startActionTouch.Equals(nullTouch))
            return;

        startActionTouch = touch;
    }

    private void SetMoveTouch(Touch touch)
    {
        if (!startMoveTouch.Equals(nullTouch))
            return;

        startMoveTouch = touch;
    }

    private bool isMoveTouch(Touch touch)
    {
        return touch.position.x <= Screen.width / 2;
    }

    public static float GetAxis(string axisName)
    {
        if (axisName == "Horizontal")
            return _horizontalAxisValue;
        if (axisName == "Vertical")
            return _verticalAxisValue;
        else
            return -1;
    }
}
