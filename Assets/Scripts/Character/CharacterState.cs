﻿using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour
{
    public enum State { Null, Idle, Movement, Attack, Jump, Grab, Throw, Stun, KnockDown, Grabbed }

    public State state;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
        if (_animator && _animator.isInitialized)
            _animator.SetInteger("Character State", (int)newState);

        if (state == State.Idle)
            if (GetComponent<Animator>())
                GetComponent<Animator>().SetFloat("PlaySpeed", 1);

        if (state == State.Movement)
        {
            if (GetComponentInChildren<AudioSource>())
                GetComponentInChildren<AudioSource>().Play();
        }
        else
        {
            if (GetComponentInChildren<AudioSource>())
                GetComponentInChildren<AudioSource>().Stop();
        }
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
