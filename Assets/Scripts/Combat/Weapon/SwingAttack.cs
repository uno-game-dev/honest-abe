class SwingAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        animator.Play("Light Attack Axe Right");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        animator.Play("Heavy Attack Axe Right");
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