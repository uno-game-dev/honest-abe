class SwingAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        animator.SetFloat("PlaySpeed", animator.GetAnimationClip("standing_melee_attack_horizontal").length / duration);
        animator.SetTrigger("Light Swing");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
        animator.SetFloat("PlaySpeed", animator.GetAnimationClip("standing_melee_attack_360_high").length / duration);
        animator.SetTrigger("Heavy Swing");
        base.PrepareToHeavyAttack();

    }

    protected override void PerformLightAttack()
    {
        if (swipe) swipe.Activate();
        base.PerformLightAttack();
    }

    protected override void PerformHeavyAttack()
    {
        if (swipe) swipe.Activate();
        base.PerformHeavyAttack();
    }

    protected override void BackToIdle()
    {
        base.BackToIdle();
    }
}