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
			animator.Play("Light Attack Saber Right");
			chain = SwingChain.Second;
		}
		else
		{
			animator.Play("Light Attack Saber Left");
			chain = SwingChain.First;
		}
	}

	protected override void PrepareToHeavyAttack()
	{
		base.PrepareToHeavyAttack();
		animator.Play("Heavy Attack Saber Stab");

		chain = SwingChain.First;
	}

	protected override void PerformLightAttack()
	{
		bool hand = chain == SwingChain.Second;
		if (swipe) swipe.Activate(hand);
		base.PerformLightAttack();
	}

	protected override void PerformHeavyAttack()
	{
		//animator.Play("Heavy Attack Saber Stab");
		bool hand = chain == SwingChain.Second;
		if (swipe) swipe.Activate(hand);
		base.PerformHeavyAttack();
	}

	protected override void BackToIdle()
	{
		base.BackToIdle();
	}
}