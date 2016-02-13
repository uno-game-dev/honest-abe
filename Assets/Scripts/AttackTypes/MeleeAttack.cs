using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MeleeAttack : MonoBehaviour, IAttackType
{
    public float prepLightAttackTime = 0.3f;
    public float prepHeavyAttackTime = 0.6f;
    public float lightAttackTime = 0.2f;
    public float heavyAttackTime = 0.2f;
    public Vector2 size = Vector2.one;
    public Vector2 position = new Vector2(1, 0.5f);
    public float damage = 10;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public Attack attack;

    [HideInInspector]
    public GameObject meleeArea;

    public void LightAttack()
    {
        animator.SetTrigger("Light Punch");
        PrepToLightAttack();
    }

    private void PrepToLightAttack()
    {
        attack.attackState = Attack.State.Prep;
        meleeArea.transform.localPosition = position;
        meleeArea.transform.localScale = size;
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
        animator.SetTrigger("Heavy Punch");
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

    public Vector2 Size { get { return size; } }
    public Vector2 Position { get { return position; } }
    public float Damage { get { return damage; } }
}