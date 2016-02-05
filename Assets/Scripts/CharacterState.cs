using UnityEngine;
using System.Collections;
using System;

public class CharacterState : MonoBehaviour
{
    public enum MovementState { Idle, Walk, Run, Jump }
    public enum AttackState { Idle, Prep, Light, Heavy }

    public MovementState movementState = MovementState.Idle;
    public AttackState attackState = AttackState.Idle;
    public MovementState previousMovementState = MovementState.Idle;
    public AttackState previousAttackState = AttackState.Idle;

    public delegate void MovementStateChangeHandler(MovementState movementState);
    public delegate void AttackStateChangeHandler(AttackState attackState);
    public event MovementStateChangeHandler MovementStateChanged = delegate { };
    public event AttackStateChangeHandler AttackStateChanged = delegate { };

    void Update()
    {
        if (previousAttackState != attackState)
        {
            AttackStateChanged(attackState);
            previousAttackState = attackState;
        }
        if (previousMovementState != movementState)
        {
            MovementStateChanged(movementState);
            previousMovementState = movementState;
        }
    }
}
