class SwingAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        animator.PlayAtSpeed("Light Attack Axe Right", 3);
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        animator.PlayAtSpeed("Heavy Attack Axe Right", 2.5f);
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