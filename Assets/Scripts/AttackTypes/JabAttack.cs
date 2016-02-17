using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JabAttack : MonoBehaviour, IAttackType
{
    public float prepLightAttackTime = 0.2f;
    public float prepHeavyAttackTime = 0.9f;
    public float lightAttackTime = 0.2f;
    public float heavyAttackTime = 0.2f;

    public void LightAttack()
    {
        Animator.SetTrigger("Light Jab");
        PrepToLightAttack();
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

    public void HeavyAttack()
    {
        Animator.SetTrigger("Heavy Jab");
        PrepToHeavyAttack();
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
        Attack.attackState = Attack.State.Idle;
        AttackArea.SetActive(false);
    }

    public Weapon Weapon { get; set; }
    public Animator Animator { get; set; }
    public Attack Attack { get; set; }
    public GameObject AttackArea { get; set; }
}