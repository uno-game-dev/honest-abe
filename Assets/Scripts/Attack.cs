using UnityEngine;
using System.Collections;
using System;

public class Attack : MonoBehaviour
{
    public enum State { Idle, Prep, Light, Heavy }

    public State attackState = State.Idle;
    public float prepLightAttackTime = 0.5f;
    public float prepHeavyAttackTime = 1f;
    public float lightAttackTime = 1f;
    public float heavyAttackTime = 1f;

    private GameObject _meleeArea;
    private Animator _animator;

    private void Awake()
    {
        _meleeArea = this.FindInChildren("Melee Area");
        _animator = GetComponent<Animator>();
    }

    public void LightAttack()
    {
        if (attackState != State.Idle)
            return;

        _animator.SetTrigger("Light Punch");
        PrepToLightAttack();
    }

    private void PrepToLightAttack()
    {
        attackState = State.Prep;
        Invoke("PerformLightAttack", prepLightAttackTime);
    }

    private void PerformLightAttack()
    {
        attackState = State.Light;
        _meleeArea.SetActive(true);
        Invoke("Disable", lightAttackTime);
    }

    public void HeavyAttack()
    {
        if (attackState != State.Idle)
            return;

        _animator.SetTrigger("Heavy Punch");
        PrepToHeavyAttack();
    }

    private void PrepToHeavyAttack()
    {
        attackState = State.Prep;
        Invoke("PerformHeavyAttack", prepHeavyAttackTime);
    }

    private void PerformHeavyAttack()
    {
        attackState = State.Heavy;
        _meleeArea.SetActive(true);
        Invoke("Disable", heavyAttackTime);
    }

    private void Disable()
    {
        attackState = State.Idle;
        _meleeArea.SetActive(false);
    }
}
