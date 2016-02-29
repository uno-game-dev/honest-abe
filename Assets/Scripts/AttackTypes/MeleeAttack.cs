using UnityEngine;

public class MeleeAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        AnimationClip clip = animator.GetAnimationClip("Punch");
        if (!clip) clip = animator.GetAnimationClip("LightSwipe");
        animator.SetFloat("PlaySpeed", clip.length / duration);
        animator.SetTrigger("Light Punch");
        base.PrepareToLightAttack();
    }

    protected override void FinishLightAttack()
    {
        base.FinishLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
        AnimationClip clip = animator.GetAnimationClip("Punch");
        if (!clip) clip = animator.GetAnimationClip("HeavySwipe");
        animator.SetFloat("PlaySpeed", clip.length / duration);
        animator.SetTrigger("Heavy Punch");
        base.PrepareToHeavyAttack();
    }

    protected override void FinishHeavyAttack()
    {
        base.FinishHeavyAttack();
    }
}