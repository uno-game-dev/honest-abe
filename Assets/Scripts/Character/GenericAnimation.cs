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
            if (state == CharacterState.State.Idle) animator.TransitionPlay("Idle Bear");
            if (state == CharacterState.State.Movement) animator.TransitionPlay("Walk Bear");
        }
        else if (name.Contains("Woman"))
        {
            if (state == CharacterState.State.Idle) animator.TransitionPlay("Woman Idle Pistol");
            if (state == CharacterState.State.Movement) animator.TransitionPlay("Woman Walk Pistol");
        }
        if (grabber && grabber.state == Grabber.State.Hold)
        {
            if (state == CharacterState.State.Idle) animator.TransitionPlay("Grab Idle");
            if (state == CharacterState.State.Movement) animator.TransitionPlay("Grab Walk");
        }
        else if (attack.weapon.attackType == Weapon.AttackType.Swing)
        {
            if (state == CharacterState.State.Idle) animator.TransitionPlay("Idle Axe");
            if (state == CharacterState.State.Movement) animator.TransitionPlay("Walk Axe");
        }
        else if (attack.weapon.attackType == Weapon.AttackType.Shoot)
        {
            if (state == CharacterState.State.Idle) animator.TransitionPlay("Idle Musket");
            if (state == CharacterState.State.Movement) animator.TransitionPlay("Walk Musket");
        }
        else
        {
            if (tag == "Player")
            {
                if (state == CharacterState.State.Idle) animator.TransitionPlay("Abe Idle");
                if (state == CharacterState.State.Movement) animator.TransitionPlay("Abe Walk");
                if (state == CharacterState.State.Dead) animator.TransitionPlay("Abe Death");
            }
            else
            {
                if (state == CharacterState.State.Idle) animator.TransitionPlay("Idle");
                if (state == CharacterState.State.Movement) animator.TransitionPlay("Walk");
            }
        }
        if (state == CharacterState.State.StartGame) animator.TransitionPlay("Knock Down On Ground");
    }
}
