using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SwingAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        animator.SetTrigger("Light Swing");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        animator.SetTrigger("Heavy Swing");
        base.PrepareToHeavyAttack();
    }
}