using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        _previousAnimationSpeed = animator.speed;
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        AnimationClip clip = animator.GetAnimationClip("Punch");
        if (!clip) clip = animator.GetAnimationClip("LightSwipe");
        animator.speed = clip.length / duration;
        animator.SetTrigger("Light Punch");
        base.PrepareToLightAttack();
    }

    protected override void FinishLightAttack()
    {
        animator.speed = _previousAnimationSpeed;
        base.FinishLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        _previousAnimationSpeed = animator.speed;
        float duration = prepHeavyAttackTime + heavyAttackTime+ finishHeavyAttackTime;
        AnimationClip clip = animator.GetAnimationClip("Punch");
        if (!clip) clip = animator.GetAnimationClip("HeavySwipe");
        animator.speed = clip.length / duration;
        animator.SetTrigger("Heavy Punch");
        base.PrepareToHeavyAttack();
    }

    protected override void FinishHeavyAttack()
    {
        animator.speed = _previousAnimationSpeed;
        base.FinishHeavyAttack();
    }
}