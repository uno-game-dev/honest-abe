using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{
    public enum Direction { Null, Left, Right }
    public enum State { Null, Idle, Walk, Grabbed, Stunned, Jump, Attack, Grab }

    public float horizontalMovementSpeed = 2;
    public float vericalMovementSpeed = 1;
    public Direction direction = Direction.Left;
    public Vector2 velocity;
    public float movementSmoothing = .115f;
    public State state;

    private BaseCollision _collision;
    private float velocityXSmoothing, velocityYSmoothing;

    private void Start()
    {
        _collision = GetComponent<BaseCollision>();
        SetDirection(direction, true);
        state = State.Idle;
    }

    public void Move(Vector2 deltaPosition)
    {
        if (state == State.Null || state == State.Grabbed || state == State.Stunned || state == State.Attack)
            return;

        if (state != State.Jump && state != State.Grab)
            if (deltaPosition.sqrMagnitude > 0)
                state = State.Walk;
            else
                state = State.Idle;

        SetDirection(deltaPosition);

        Vector3 velocity = deltaPosition;

        if (Mathf.Abs(deltaPosition.x) < horizontalMovementSpeed)
            velocity.x = Mathf.SmoothDamp(deltaPosition.x, velocity.x, ref velocityXSmoothing, movementSmoothing);
        else
            velocity.x = Mathf.Sign(deltaPosition.x) * horizontalMovementSpeed;

        if (Mathf.Abs(deltaPosition.y) < vericalMovementSpeed)
            velocity.y = Mathf.SmoothDamp(deltaPosition.y, velocity.y, ref velocityYSmoothing, movementSmoothing);
        else
            velocity.y = Mathf.Sign(deltaPosition.y) * vericalMovementSpeed;

        this.velocity = velocity;
        _collision.Move(velocity * Time.deltaTime);
    }

    private void SetDirection(Vector2 deltaPosition)
    {
        if (deltaPosition.x > 0)
            SetDirection(Direction.Right);
        else if (deltaPosition.x < 0)
            SetDirection(Direction.Left);
    }

    public void SetDirection(Direction newDirection, bool forceUpdate = false)
    {
        if (direction == newDirection && !forceUpdate)
            return;

        direction = newDirection;
        Vector3 newScale = transform.localScale;
        if (direction == Direction.Left)
            newScale.x = -Math.Abs(transform.localScale.x);
        else
            newScale.x = Math.Abs(transform.localScale.x);
        transform.localScale = newScale;
    }

    public void FlipDirection()
    {
        if (direction == Direction.Left)
            SetDirection(Direction.Right, true);
        else if (direction == Direction.Right)
            SetDirection(Direction.Left, true);
    }
}
