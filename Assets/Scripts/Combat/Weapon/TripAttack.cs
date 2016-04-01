using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TripAttack : BaseAttack
{
	// For bushwhacker, Heavy attack = trip, Light attack = knives
	protected override void PrepareToLightAttack()
	{
		float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
		animator.SetFloat("PlaySpeed", animator.GetAnimationClip("standing_melee_attack_horizontal").length / duration);
		animator.SetTrigger("Knife Attack");
		base.PrepareToLightAttack();
	}

	protected override void PrepareToHeavyAttack()
	{
		float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
		animator.SetFloat("PlaySpeed", animator.GetAnimationClip("standing_melee_attack_360_high").length / duration);
		animator.SetTrigger("Trip");
		base.PrepareToHeavyAttack();
	}

	protected override void BackToIdle()
	{
		base.BackToIdle();
	}
}