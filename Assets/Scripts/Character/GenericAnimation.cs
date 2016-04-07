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
            if (state == CharacterState.State.Grab)
            {
                if (state == CharacterState.State.Idle) animator.PlayAtSpeed("Grab Idle");
                if (state == CharacterState.State.Movement) animator.PlayAtSpeed("Grab Walk");
            }
            if (attack.weapon.attackType == Weapon.AttackType.Swing)
            {
                if (state == CharacterState.State.Idle) animator.PlayAtSpeed("Idle Axe");
                if (state == CharacterState.State.Movement) animator.PlayAtSpeed("Walk Axe", 4);
            }
            else if (attack.weapon.attackType == Weapon.AttackType.Shoot)
            {
                if (state == CharacterState.State.Idle) animator.PlayAtSpeed("Idle Musket");
                if (state == CharacterState.State.Movement) animator.PlayAtSpeed("Walk Musket", 4);
            }
            else
            {
                if (state == CharacterState.State.Idle) animator.PlayAtSpeed("Idle");
                if (state == CharacterState.State.Movement) animator.PlayAtSpeed("Walk", 4);
            }
            if (state == CharacterState.State.Dead) animator.PlayAtSpeed("Dead", 0.1f);
        }
    }
}
