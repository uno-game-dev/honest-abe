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
        animator.Play("Knife Stab Ground Left");
		base.PrepareToLightAttack();
	}

	protected override void PrepareToHeavyAttack()
	{
        animator.Play("Trip Attack");
		base.PrepareToHeavyAttack();
	}

	protected override void BackToIdle()
	{
		base.BackToIdle();
	}
}