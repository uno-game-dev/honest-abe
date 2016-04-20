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
        animator.Play("Heavy Attack Axe Prep");

        chain = SwingChain.First;
        SoundPlayer.Play("Heavy Axe Swing");
    }

    protected override void PerformLightAttack()
    {
        bool hand = chain == SwingChain.Second;
        if (swipe) swipe.Activate(hand);
        base.PerformLightAttack();
    }

    protected override void PerformHeavyAttack()
    {
        animator.Play("Heavy Attack Axe Swing");
        bool hand = chain == SwingChain.Second;
        if (swipe) swipe.Activate(hand);
        base.PerformHeavyAttack();
    }

    protected override void BackToIdle()
    {
        base.BackToIdle();
    }
}