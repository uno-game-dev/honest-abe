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
        animator.TransitionPlay("Light Attack Stab");
    }

    protected override void PrepareToHeavyAttack()
    {
        base.PrepareToHeavyAttack();
        animator.TransitionPlay("Heavy Attack Stab");
    }

    protected override void BackToIdle()
    {
        base.BackToIdle();
    }
}