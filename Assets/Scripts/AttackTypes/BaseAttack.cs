using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public enum AttackStrength { Light, Heavy, Throw }

    public Weapon weapon;
    public Animator animator;
    public Attack attack;
    public GameObject attackArea;
    public float prepLightAttackTime = 0.3f;
    public float prepHeavyAttackTime = 0.6f;
    public float lightAttackTime = 0.2f;
    public float heavyAttackTime = 0.2f;

    public virtual void StartAttack(AttackStrength strength)
    {
        if (attack.attackState != Attack.State.Idle)
            return;

        attackArea.transform.localPosition = weapon.attackOffset;
        attackArea.transform.localScale = weapon.attackSize;

        if (strength == AttackStrength.Light)
            PrepToLightAttack();
        else if (strength == AttackStrength.Heavy)
            PrepToHeavyAttack();
    }

    protected virtual void PrepToLightAttack()
    {
        attack.attackState = Attack.State.Prep;
        Invoke("PerformLightAttack", prepLightAttackTime);
    }

    protected virtual void PrepToHeavyAttack()
    {
        attack.attackState = Attack.State.Prep;
        Invoke("PerformHeavyAttack", prepHeavyAttackTime);
    }

    protected virtual void PerformLightAttack()
    {
        attack.attackState = Attack.State.Light;
        attackArea.SetActive(true);
        Invoke("Disable", lightAttackTime);
    }

    protected virtual void PerformHeavyAttack()
    {
        attack.attackState = Attack.State.Heavy;
        attackArea.SetActive(true);
        Invoke("Disable", heavyAttackTime);
    }

    protected virtual void Disable()
    {
        attackArea.SetActive(false);
        attack.attackState = Attack.State.Idle;
    }
}