using UnityEngine;
using System.Collections;

public class KnockDown : MonoBehaviour
{
    public const float GRAVITY = -9.81f;
    public enum State { Null, InAir, OnGround, GettingUp }

    public State state;
    public float height;
    public float horizontalVelocity = 10;
    public float gravityMultiplier = 1;
    public float onGroundDuration = 1;
    public float getUpDuration = 1;
    private CharacterState _characterState;
    private Animator _animator;
    private float sign = 1;

    private void Awake()
    {
        _characterState = GetComponent<CharacterState>();
        _animator = GetComponent<Animator>();
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
        if (state == State.OnGround)
        {
            horizontalVelocity += GRAVITY * Time.deltaTime * gravityMultiplier;
            horizontalVelocity = Mathf.Clamp(horizontalVelocity, 0, horizontalVelocity);
            transform.Translate(sign * horizontalVelocity * Time.deltaTime, 0, 0);
        }
    }

    public void StartKnockDown(float horizontalVelocity)
    {
        if (state != State.Null)
            return;

        sign = Mathf.Sign(horizontalVelocity);
        this.horizontalVelocity = Mathf.Abs(horizontalVelocity);
        height = 5;
        SetState(State.InAir);
        _animator.SetTrigger("KnockDown");
        _characterState.SetState(CharacterState.State.KnockDown);
    }

    private void HitGround()
    {
        height = 0;
        SetState(State.OnGround);
        Invoke("GetUp", onGroundDuration);
    }

    private void GetUp()
    {
        SetState(State.GettingUp);
        Invoke("BackToIdle", getUpDuration);
    }

    private void BackToIdle()
    {
        SetState(State.Null);
        _characterState.SetState(CharacterState.State.Idle);
    }

    private void SetState(State newState)
    {
        state = newState;
        _animator.SetInteger("KnockDownState", (int)state);
    }
}
