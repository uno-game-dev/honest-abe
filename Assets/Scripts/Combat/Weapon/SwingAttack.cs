class SwingAttack : BaseAttack
{
    public enum SwingChain { First, Second, Third }
    public SwingChain chain;

    protected override void PrepareToLightAttack()
    {
        base.PrepareToLightAttack();
        if (chain == SwingChain.First)
        {
            animator.Play("Light Attack Axe Right");
            chain = SwingChain.Second;
        }
        else
        {
            animator.Play("Light Attack Axe Left");
            chain = SwingChain.First;
        }
        SoundPlayer.Play("Light Axe Swing");
    }

    protected override void PrepareToHeavyAttack()
    {
        base.PrepareToHeavyAttack();
        if (chain == SwingChain.First)
            animator.Play("Heavy Attack Axe Right");
        else
            animator.Play("Heavy Attack Axe Left");

        chain = SwingChain.First;
        SoundPlayer.Play("Heavy Axe Swing");
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