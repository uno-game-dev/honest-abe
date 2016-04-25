using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class CharacterState : MonoBehaviour
{
    public enum State { Null, Idle, Movement, Attack, Jump, Grab, Throw, Stun, KnockDown, Grabbed, Dead, Cinematic, Pickup, StartGame }

    public State state;

    private Animator animator;
    public StateMachine stateMachine;
    private bool previouslyDisabledByThisScript;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
    }

    private void OnEnable()
    {
        if (tag != "Player")
            SetState(State.Idle);
    }

    private void OnDisable()
    {
        SetState(State.Null);
    }

    public void SetState(State newState)
    {
        if (newState == State.Idle && state == State.Grabbed)
            return;

        if (state == State.Dead)
            return;

        if (stateMachine)
        {
            if (newState == State.Grabbed || newState == State.KnockDown || newState == State.Stun || newState == State.Dead)
            {
                previouslyDisabledByThisScript = true;
                stateMachine.enabled = false;
            }
            else if (previouslyDisabledByThisScript)
            {
                stateMachine.enabled = true;
                previouslyDisabledByThisScript = false;
            }
        }


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
        return state == State.Idle || state == State.Movement || state == State.Grab || state == State.Attack || state == State.Stun;
    }

    public bool CanBeStunned()
    {
        return state == State.Idle || state == State.Movement || state == State.Grab || state == State.Attack || state == State.Pickup
            || state == State.Throw;
    }
}
