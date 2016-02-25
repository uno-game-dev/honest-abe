using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public enum State { Null, StartJump, JumpUp, Fall, Land }

    public float simulatedHeight = 0;
    public float gravityMultiplier = 8;
    public float jumpStrength = 20;
    public bool isGrounded = true;
    public float jumpVelocity;
    public State state;
    public float startJumpDuration = 0.1f;
    public float landDuration = 0.1f;

    private float gravity = -9.81f;
    private float _previousHeight;
    private BaseCollision _collision;
    private Movement _movement;
    private Animator _animator;
    private float _previousAnimationSpeed;

    private void Start()
    {
        _collision = GetComponent<BaseCollision>();
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        SetState(State.Null);
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
    }

    private void CheckGrounded()
    {
        if (!isGrounded && _previousHeight == simulatedHeight)
            Land();
        else if (_previousHeight != simulatedHeight)
        {
            isGrounded = false;
            if (jumpVelocity < 0)
                SetState(State.Fall);
        }
    }

    public void StartJump()
    {
        if (_movement.state != Movement.State.Idle && _movement.state != Movement.State.Walk)
            return;

        if (!isGrounded)
            return;

        SetState(State.StartJump);

        _previousAnimationSpeed = _animator.speed;
        _animator.speed = _animator.GetAnimationClip("standing_jump-start").length / startJumpDuration;

        _movement.state = Movement.State.Null;
        Invoke("PerformJump", startJumpDuration);
    }

    private void PerformJump()
    {
        SetState(State.JumpUp);
        _movement.state = Movement.State.Jump;
        _animator.speed = _previousAnimationSpeed;
        jumpVelocity = jumpStrength;
        isGrounded = false;
        _collision.collisionLayer ^= (1 << LayerMask.NameToLayer("Environment"));
        _collision.collisionLayer ^= (1 << LayerMask.NameToLayer("Enemy"));
    }

    private void Land()
    {
        SetState(State.Land);

        _previousAnimationSpeed = _animator.speed;
        _animator.speed = _animator.GetAnimationClip("standing_jump-land").length / landDuration;

        _movement.state = Movement.State.Null;
        isGrounded = true;
        _collision.collisionLayer = _collision.collisionLayer | (1 << LayerMask.NameToLayer("Environment"));
        _collision.collisionLayer = _collision.collisionLayer | (1 << LayerMask.NameToLayer("Enemy"));
        Invoke("FinishLand", landDuration);
    }

    private void FinishLand()
    {
        SetState(State.Null);
        _animator.speed = _previousAnimationSpeed;
        if (_movement.state == Movement.State.Null)
            _movement.state = Movement.State.Idle;
    }

    private void SetState(State newState)
    {
        _animator.SetInteger("Jump", (int)newState);
        state = newState;
    }
}
