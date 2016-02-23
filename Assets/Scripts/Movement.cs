using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{
    public enum Direction { Null, Left, Right }

    public float horizontalMovementSpeed = 2;
    public float vericalMovementSpeed = 1;
    public float simulatedHeight = 0;
    public float gravityMultiplier = 8;
    public float jumpStrength = 20;
    public Direction direction = Direction.Left;
    public Vector2 velocity;
    public float movementSmoothing = .115f;
    public float yToZFactor = 2f;

    [HideInInspector]
    public bool isGrounded;

    [HideInInspector]
    public float jumpVelocity;

    private float gravity = -9.81f;
    private float _previousHeight;
    private BaseCollision _collision;
    private float velocityXSmoothing, velocityYSmoothing;
    private Attack _attack;

    private void Start()
    {
        _collision = GetComponent<BaseCollision>();
        _attack = GetComponent<Attack>();
        SetDirection(direction, true);
    }

    private void Update()
    {
        simulatedHeight += jumpVelocity * Time.deltaTime;
        simulatedHeight = Mathf.Clamp(simulatedHeight, 0, simulatedHeight);

        if (jumpVelocity <= 0)
            CheckGrounded();

        _previousHeight = simulatedHeight;

        jumpVelocity += gravity * gravityMultiplier * Time.deltaTime;
        jumpVelocity = simulatedHeight <= 0 ? 0 : jumpVelocity;

        ChangeZBasedOnY();
    }

    private void ChangeZBasedOnY()
    {
        Vector3 localPosition = transform.localPosition;
        localPosition.z = localPosition.y * yToZFactor;
        transform.localPosition = localPosition;
    }

    private void CheckGrounded()
    {
        if (_previousHeight == simulatedHeight) {
            isGrounded = true;
            _collision.collisionLayer |= (1 << LayerMask.NameToLayer("Environment"));
        }
        else
            isGrounded = false;
    }

    public void Jump()
    {
        if (_attack.attackState == Attack.State.Grab)
            return;

        if (!isGrounded)
            return;

        jumpVelocity = jumpStrength;
        isGrounded = false;
        _collision.collisionLayer &= ~(1 << LayerMask.NameToLayer("Environment"));
    }

    public void Move(Vector2 deltaPosition)
    {
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

    public void Flip()
    {
        if (direction == Direction.Left)
            SetDirection(Direction.Right, true);
        else if (direction == Direction.Right)
            SetDirection(Direction.Left, true);
    }
}
