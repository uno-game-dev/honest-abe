using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public float prepLightAttackTime = 0.3f;
    public float prepHeavyAttackTime = 0.6f;
    public float lightAttackTime = 0.2f;
    public float heavyAttackTime = 0.2f;

    public override void AddAttack(AttackStrength strength)
    {
        if (input.Count >= maxQueueSize)
            return;

        input.Enqueue(strength);
        if (Attack.attackState == Attack.State.Idle)
            ProcessNextAttack();
    }

    private void ProcessNextAttack()
    {
        if (input.Count <= 0)
            return;

        AttackStrength strength = input.Dequeue();
        if (strength == AttackStrength.Light)
        {
            Animator.SetTrigger("Light Punch");
            PrepToLightAttack();
        }
        else
        {
            Animator.SetTrigger("Heavy Punch");
            PrepToHeavyAttack();
        }
    }

    private void PrepToLightAttack()
    {
        Attack.attackState = Attack.State.Prep;
        AttackArea.transform.localPosition = Weapon.attackOffset;
        AttackArea.transform.localScale = Weapon.attackSize;
        Invoke("PerformLightAttack", prepLightAttackTime);
    }

    private void PerformLightAttack()
    {
        Attack.attackState = Attack.State.Light;
        AttackArea.SetActive(true);
        Invoke("Disable", lightAttackTime);
    }

    private void PrepToHeavyAttack()
    {
        Attack.attackState = Attack.State.Prep;
        Invoke("PerformHeavyAttack", prepHeavyAttackTime);
    }

    private void PerformHeavyAttack()
    {
        Attack.attackState = Attack.State.Heavy;
        AttackArea.SetActive(true);
        Invoke("Disable", heavyAttackTime);
    }

    private void Disable()
    {
        if (AttackArea.GetComponent<AttackArea>().hit == null)
            input.Clear();

        AttackArea.SetActive(false);

        if (input.Count > 0)
            ProcessNextAttack();
        else
            Attack.attackState = Attack.State.Idle;
    }
}