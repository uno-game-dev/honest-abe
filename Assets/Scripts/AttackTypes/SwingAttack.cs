using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SwingAttack : BaseAttack
{
    protected override void PrepToLightAttack()
    {
        animator.SetTrigger("Light Swing");
        base.PrepToLightAttack();
    }

    protected override void PrepToHeavyAttack()
    {
        animator.SetTrigger("Heavy Swing");
        base.PrepToHeavyAttack();
    }
}