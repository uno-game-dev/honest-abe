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
    }

    protected override void PrepareToHeavyAttack()
    {
        if (tag == "Player")
            SoundPlayer.Play("Heavy Axe Lift");

        base.PrepareToHeavyAttack();
        animator.Play("Heavy Attack Axe Prep");
        chain = SwingChain.First;
    }

    protected override void PerformLightAttack()
    {
        if (tag == "Player")
            SoundPlayer.Play("Light Axe Swing");

        bool hand = chain == SwingChain.Second;
        if (swipe) swipe.Activate(hand);
        base.PerformLightAttack();
    }

    protected override void PerformHeavyAttack()
    {
        if (tag == "Player")
            SoundPlayer.Play("Heavy Axe Swing");

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