using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JabAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        animator.SetFloat("PlaySpeed", animator.GetAnimationClip("standing_melee_attack_horizontal").length / duration);
        animator.SetTrigger("Light Jab");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
        animator.SetFloat("PlaySpeed", animator.GetAnimationClip("standing_melee_attack_360_high").length / duration);
        animator.SetTrigger("Heavy Jab");
        base.PrepareToHeavyAttack();
    }

    protected override void BackToIdle()
    {
        base.BackToIdle();
    }
}