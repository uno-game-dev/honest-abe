using UnityEngine;

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
    private CharacterState _characterState;

    private void Start()
    {
        _collision = GetComponent<BaseCollision>();
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _characterState = GetComponent<CharacterState>();
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
                _animator.TransitionPlay("Jump Fall");
        }
    }

    public void StartJump()
    {
        if (!_characterState.CanJump())
            return;

        if (!isGrounded)
            return;

        EventHandler.SendEvent(EventHandler.Events.JUMP);
        SetState(State.StartJump);
        _animator.TransitionPlay("Jump Start");
        SoundPlayer.Play("Jump Start");
        _characterState.SetState(CharacterState.State.Null);
        Invoke("PerformJump", startJumpDuration);
    }

    private void PerformJump()
    {
        SetState(State.JumpUp);
        _characterState.SetState(CharacterState.State.Jump);
        jumpVelocity = jumpStrength;
        isGrounded = false;
        _animator.TransitionPlay("Jump Up");
        SoundPlayer.Play("Jump Up");
        _collision.RemoveCollisionLayer("Environment");
        _collision.RemoveCollisionLayer("Enemy");
    }

    private void Land()
    {
        EventHandler.SendEvent(EventHandler.Events.LAND);
        SetState(State.Land);
        _animator.TransitionPlay("Jump Land");
        SoundPlayer.Play("Jump Land");
        _characterState.SetState(CharacterState.State.Null);
        isGrounded = true;
        _collision.AddCollisionLayer("Environment");
        _collision.AddCollisionLayer("Enemy");
        Invoke("FinishLand", landDuration);
    }

    private void FinishLand()
    {
        SetState(State.Null);
        _characterState.SetState(CharacterState.State.Idle);
    }

    private void SetState(State newState)
    {
        state = newState;
    }
}
