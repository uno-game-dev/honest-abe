using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SwingAttack : MonoBehaviour, IAttackType
{
    public float prepLightAttackTime = 0.2f;
    public float prepHeavyAttackTime = 0.9f;
    public float lightAttackTime = 0.2f;
    public float heavyAttackTime = 0.2f;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public Attack attack;

    [HideInInspector]
    public GameObject meleeArea;

    public void LightAttack()
    {
        animator.SetTrigger("Light Swing");
        PrepToLightAttack();
    }

    private void PrepToLightAttack()
    {
        attack.attackState = Attack.State.Prep;
        meleeArea.transform.localPosition = Weapon.attackOffset;
        meleeArea.transform.localScale = Weapon.attackSize;
        Invoke("PerformLightAttack", prepLightAttackTime);
    }

    private void PerformLightAttack()
    {
        attack.attackState = Attack.State.Light;
        meleeArea.SetActive(true);
        Invoke("Disable", lightAttackTime);
    }

    public void HeavyAttack()
    {
        animator.SetTrigger("Heavy Swing");
        PrepToHeavyAttack();
    }

    private void PrepToHeavyAttack()
    {
        attack.attackState = Attack.State.Prep;
        Invoke("PerformHeavyAttack", prepHeavyAttackTime);
    }

    private void PerformHeavyAttack()
    {
        attack.attackState = Attack.State.Heavy;
        meleeArea.SetActive(true);
        Invoke("Disable", heavyAttackTime);
    }

    private void Disable()
    {
        attack.attackState = Attack.State.Idle;
        meleeArea.SetActive(false);
    }

    public Weapon Weapon { get; set; }
}