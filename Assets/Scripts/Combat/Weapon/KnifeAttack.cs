using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class KnifeAttack : BaseAttack
{
	// For bushwhacker, Heavy attack = trip, Light attack = knives
	protected override void PrepareToLightAttack()
	{
		base.PrepareToLightAttack();
        animator.PlayAtSpeed("Light Attack Knife");
	}

	protected override void PrepareToHeavyAttack()
	{
		base.PrepareToHeavyAttack();
        animator.PlayAtSpeed("Trip Attack 0");
	}

	protected override void BackToIdle()
	{
		base.BackToIdle();
	}
}