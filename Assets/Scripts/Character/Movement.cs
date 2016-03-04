using UnityEngine;
using System;

[RequireComponent(typeof(CharacterState))]
public class Movement : MonoBehaviour
{
    public enum State { Null, Walk }
    public enum Direction { Null, Left, Right }

    public float horizontalMovementSpeed = 2;
    public float vericalMovementSpeed = 1;
    public Direction direction = Direction.Left;
    public Vector2 velocity;
    public float movementSmoothing = .115f;
    public State state;

    private BaseCollision _collision;
    private float velocityXSmoothing, velocityYSmoothing;
    private CharacterState _characterState;

    private void Awake()
    {
        _characterState = GetComponent<CharacterState>();
        _collision = GetComponent<BaseCollision>();
        SetDirection(direction, true);
    }

    public void Move(Vector2 deltaPosition)
    {
        if (!_characterState.CanMove())
            return;

        if (deltaPosition != Vector2.zero)
            SetState(State.Walk);
        else
            SetState(State.Null);

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

    private void SetState(State newState)
    {
        state = newState;

        if (!_characterState.CanMove())
            return;

        if (newState == State.Walk && _characterState.state == CharacterState.State.Idle)
            _characterState.SetState(CharacterState.State.Movement);
        else if (newState == State.Null && _characterState.state == CharacterState.State.Movement)
            _characterState.SetState(CharacterState.State.Idle);
    }
}
