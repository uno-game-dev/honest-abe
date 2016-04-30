using UnityEngine;
using System.Collections;

class SlashAttack : BaseAttack
{
	public enum SwingChain { First, Second, Third }
	public SwingChain chain;

	protected override void PrepareToLightAttack()
	{
		base.PrepareToLightAttack();
		if (chain == SwingChain.First)
		{
			animator.TransitionPlay("Light Attack Saber Right");
			chain = SwingChain.Second;
		}
		else
		{
			animator.TransitionPlay("Light Attack Saber Left");
			chain = SwingChain.First;
		}
	}

	protected override void PrepareToHeavyAttack()
	{
		base.PrepareToHeavyAttack();
		animator.TransitionPlay("Heavy Attack Saber Stab");
		chain = SwingChain.First;
	}

	protected override void PerformLightAttack()
	{
        SoundPlayer.Play("Saber Swing");
        bool hand = chain == SwingChain.Second;
		if (swipe) swipe.Activate(hand);
		base.PerformLightAttack();
	}

	protected override void PerformHeavyAttack()
	{
        SoundPlayer.Play("Saber Swing");
        bool hand = chain == SwingChain.Second;
		if (swipe) swipe.Activate(hand);
		base.PerformHeavyAttack();
	}

	protected override void BackToIdle()
	{
		base.BackToIdle();
	}
}