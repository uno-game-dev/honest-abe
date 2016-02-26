using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JabAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        _previousAnimationSpeed = animator.speed;
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        animator.speed = animator.GetAnimationClip("standing_melee_attack_horizontal").length / duration;

        animator.SetTrigger("Light Jab");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        _previousAnimationSpeed = animator.speed;
        float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
        animator.speed = animator.GetAnimationClip("standing_melee_attack_360_high").length / duration;

        animator.SetTrigger("Heavy Jab");
        base.PrepareToHeavyAttack();
    }

    protected override void BackToIdle()
    {
        animator.speed = _previousAnimationSpeed;
        base.BackToIdle();
    }
}