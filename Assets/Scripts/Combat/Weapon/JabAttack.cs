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
        animator.Play("Light Attack Stab");
    }

    protected override void PrepareToHeavyAttack()
    {
        base.PrepareToHeavyAttack();
        animator.Play("Heavy Attack Stab");
    }

    protected override void BackToIdle()
    {
        base.BackToIdle();
    }
}