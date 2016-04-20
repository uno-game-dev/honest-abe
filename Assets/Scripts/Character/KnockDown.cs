using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class KnockDown : MonoBehaviour
{
    public const float GRAVITY = -9.81f;
    public enum State { Null, InAir, Land, OnGround, GettingUp }

    public State state;
    public float height;
    public float horizontalVelocity = 10;
    public float gravityMultiplier = 1;
    public float landDuration = 0.5f;
    public float onGroundDuration = 1;
    public float getUpDuration = 1;
    private CharacterState _characterState;
    private Animator _animator;
	private Movement _movement;
    private float sign = 1;
    private StateMachine stateMachine;

    private void Awake()
    {
        _characterState = GetComponent<CharacterState>();
        _animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
		_movement = GetComponent<Movement>();
	}

    private void Update()
    {
        if (state == State.Null)
            return;

        if (state == State.InAir)
        {
            transform.Translate(sign * horizontalVelocity * Time.deltaTime, 0, 0);
            height += GRAVITY * Time.deltaTime * gravityMultiplier;

            if (height <= 0)
                HitGround();
        }
        if (state == State.OnGround || state == State.Land)
        {
			if (_movement)
				_movement.enabled = false;
            horizontalVelocity += GRAVITY * Time.deltaTime * gravityMultiplier;
            horizontalVelocity = Mathf.Clamp(horizontalVelocity, 0, horizontalVelocity);
            transform.Translate(sign * horizontalVelocity * Time.deltaTime, 0, 0);
        }
		else
		{
			if (_movement)
				_movement.enabled = true;
		}
    }

    public void StartKnockDown(float horizontalVelocity)
    {
        if (state != State.Null)
            return;

        if (_characterState.state == CharacterState.State.Dead)
            return;

        sign = Mathf.Sign(horizontalVelocity);
        this.horizontalVelocity = Mathf.Abs(horizontalVelocity);
        height = 5;
        SetState(State.InAir);
        _animator.Play("Knock Down In Air");
        if (stateMachine) stateMachine.enabled = false;
        _characterState.SetState(CharacterState.State.KnockDown);
    }

    private void HitGround()
    {
        height = 0;
        SetState(State.Land);
        _animator.Play("Knock Down Land");
        Invoke("Land", landDuration);
    }

    private void Land()
    {
        SetState(State.OnGround);
        _animator.Play("Knock Down On Ground");
        Invoke("GetUp", onGroundDuration);
    }

    private void GetUp()
    {
        SetState(State.GettingUp);
        _animator.Play("Knock Down Get Up");
        Invoke("BackToIdle", getUpDuration);
    }

    private void BackToIdle()
    {
        SetState(State.Null);
        if (stateMachine) stateMachine.enabled = true;
        _characterState.SetState(CharacterState.State.Idle);
    }

    private void SetState(State newState)
    {
        state = newState;
    }
}
