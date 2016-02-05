using UnityEngine;
using System.Collections;
using System;

public class Attack : MonoBehaviour
{
    public float prepLightAttackTime = 0.5f;
    public float prepHeavyAttackTime = 1f;
    public float lightAttackTime = 1f;
    public float heavyAttackTime = 1f;

    private GameObject _meleeArea;
    private CharacterState _characterState;

    private void Awake()
    {
        _meleeArea = this.FindInChildren("Melee Area");
        _characterState = GetComponent<CharacterState>();
    }

    private void OnEnable()
    {
        MainCameraKeyboardController.LightAttack += OnLightAttack;
        MainCameraKeyboardController.HeavyAttack += OnHeavyAttack;
    }

    private void OnDisable()
    {
        MainCameraKeyboardController.LightAttack -= OnLightAttack;
        MainCameraKeyboardController.HeavyAttack -= OnHeavyAttack;
    }

    private void OnLightAttack()
    {
        if (_characterState.attackState != CharacterState.AttackState.Idle)
            return;

        PrepToLightAttack();
    }

    private void OnHeavyAttack()
    {
        if (_characterState.attackState != CharacterState.AttackState.Idle)
            return;

        PrepToHeavyAttack();
    }

    private void PrepToLightAttack()
    {
        _characterState.attackState = CharacterState.AttackState.Prep;
        Invoke("LightAttack", prepLightAttackTime);
    }

    private void PrepToHeavyAttack()
    {
        _characterState.attackState = CharacterState.AttackState.Prep;
        Invoke("HeavyAttack", prepHeavyAttackTime);
    }

    private void LightAttack()
    {
        _characterState.attackState = CharacterState.AttackState.Light;
        _meleeArea.SetActive(true);
        Invoke("Disable", lightAttackTime);
    }

    private void HeavyAttack()
    {
        _characterState.attackState = CharacterState.AttackState.Heavy;
        _meleeArea.SetActive(true);
        Invoke("Disable", heavyAttackTime);
    }

    private void Disable()
    {
        _characterState.attackState = CharacterState.AttackState.Idle;
        _meleeArea.SetActive(false);
    }
}
