using UnityEngine;
using System.Collections;
using System;

public class GenericAnimation : MonoBehaviour
{
    private CharacterState characterState;
    private Animator animator;
    private Attack attack;

    private CharacterState.State previousState;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterState = GetComponent<CharacterState>();
        attack = GetComponent<Attack>();
    }

    void Update()
    {
        CharacterState.State state = characterState.state;
        if (previousState != state)
        {
            previousState = characterState.state;
            if (name == "Bear")
            {
                if (state == CharacterState.State.Idle) animator.Play("Idle Bear");
                if (state == CharacterState.State.Movement) animator.Play("Walk Bear");
            }
            if (state == CharacterState.State.Grab)
            {
                if (state == CharacterState.State.Idle) animator.Play("Grab Idle");
                if (state == CharacterState.State.Movement) animator.Play("Grab Walk");
            }
            if (attack.weapon.attackType == Weapon.AttackType.Swing)
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
                if (state == CharacterState.State.Idle) animator.Play("Idle");
                if (state == CharacterState.State.Movement) animator.Play("Walk");
            }
            if (state == CharacterState.State.Dead) animator.Play("Dead");
        }
    }
}
