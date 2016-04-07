using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JabAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        base.PrepareToLightAttack();
        animator.PlayAtSpeed("Light Attack Stab", 1.25f);
    }

    protected override void PrepareToHeavyAttack()
    {
        base.PrepareToHeavyAttack();
        animator.PlayAtSpeed("Heavy Attack Stab", 3);
    }

    protected override void BackToIdle()
    {
        base.BackToIdle();
    }
}