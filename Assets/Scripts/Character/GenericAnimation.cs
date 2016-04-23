using UnityEngine;
using System.Collections;
using System;

public class GenericAnimation : MonoBehaviour
{
    private CharacterState characterState;
    private Animator animator;
    private Attack attack;

    private CharacterState.State previousState;
    public Grabber grabber;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterState = GetComponent<CharacterState>();
        attack = GetComponent<Attack>();
        grabber = GetComponent<Grabber>();
    }

    void Update()
    {
        CharacterState.State state = characterState.state;
        if (previousState != state)
        {
            previousState = state;
            UpdateState();
        }
    }

    public void UpdateState()
    {
        CharacterState.State state = characterState.state;
        if (animator.runtimeAnimatorController.name == "Bear")
        {
            if (state == CharacterState.State.Idle) animator.Play("Idle Bear");
            if (state == CharacterState.State.Movement) animator.Play("Walk Bear");
        }
        if (grabber && grabber.state == Grabber.State.Hold)
        {
            if (state == CharacterState.State.Idle) animator.Play("Grab Idle");
            if (state == CharacterState.State.Movement) animator.Play("Grab Walk");
        }
        else if (attack.weapon.attackType == Weapon.AttackType.Swing)
        {
            if (state == CharacterState.State.Idle) animator.Play("Idle Axe");
            if (state == CharacterState.State.Movement) animator.Play("Walk Axe");
        }
        else if (attack.weapon.attackType == Weapon.AttackType.Shoot)
        {
            if (state == CharacterState.State.Idle) animator.Play("Idle Musket");
            if (state == CharacterState.State.Movement) animator.Play("Walk Musket");
        }
        else
        {
            if (tag == "Player")
            {
                if (state == CharacterState.State.Idle) animator.Play("Abe Idle");
                if (state == CharacterState.State.Movement) animator.Play("Abe Walk");
            }
            else
            {
                if (state == CharacterState.State.Idle) animator.Play("Idle");
                if (state == CharacterState.State.Movement) animator.Play("Walk");
            }
        }
        if (state == CharacterState.State.StartGame) animator.Play("Knock Down On Ground");
    }
}
