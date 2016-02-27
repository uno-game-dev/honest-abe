using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour
{
    public enum State { Null, Idle, Movement, Attack, Jump, Grab, Throw, Stun, KnockDown, Grabbed }

    public State state;

    private void OnEnable()
    {
        SetState(State.Idle);
    }

    private void OnDisable()
    {
        SetState(State.Null);
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    public bool CanMove()
    {
        return state == State.Idle || state == State.Movement || state == State.Jump || state == State.Grab;
    }

    public bool CanAttack()
    {
        return state == State.Idle || state == State.Movement;
    }

    public bool CanJump()
    {
        return state == State.Idle || state == State.Movement;
    }

    public bool CanGrab()
    {
        return state == State.Idle || state == State.Movement;
    }

    public bool CanThrow()
    {
        return state == State.Idle || state == State.Movement;
    }

    public bool CanBeGrabbed()
    {
        return state != State.KnockDown;
    }
}
