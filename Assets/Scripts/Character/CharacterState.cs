﻿using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour
{
    public enum State { Null, Idle, Movement, Attack, Jump, Grab, Throw, Stun, KnockDown, Grabbed, Dead, Cinematic, Pickup }

    public State state;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
}
