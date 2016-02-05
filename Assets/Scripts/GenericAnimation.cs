using UnityEngine;
using System.Collections;
using System;

public class GenericAnimation : MonoBehaviour
{
    private Animator _animator;
    private BaseCollision _baseCollision;
    private CharacterState _chatacterState;
    private Jump _jump;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _baseCollision = GetComponent<BaseCollision>();
        _jump = GetComponent<Jump>();
        _chatacterState = GetComponent<CharacterState>();

        if (_chatacterState)
        {
            MainCameraKeyboardController.LightAttack += OnLightAttack;
            MainCameraKeyboardController.HeavyAttack += OnHeavyAttack;
        }
    }

    private void OnLightAttack()
    {
        if (_chatacterState.attackState == CharacterState.AttackState.Idle 
            || _chatacterState.previousAttackState == CharacterState.AttackState.Idle)
            _animator.SetTrigger("Light Punch");
    }

    private void OnHeavyAttack()
    {
        if (_chatacterState.attackState == CharacterState.AttackState.Idle
            || _chatacterState.previousAttackState == CharacterState.AttackState.Idle)
            _animator.SetTrigger("Heavy Punch");
    }

    void Update()
    {
        _animator.SetFloat("Horizontal Velocity", _baseCollision.Velocity.x / Time.deltaTime);
        _animator.SetFloat("Vertical Velocity", _baseCollision.Velocity.y / Time.deltaTime);

        if (_jump)
            _animator.SetFloat("Jump Velocity", _jump.jumpVelocity);
    }
}
