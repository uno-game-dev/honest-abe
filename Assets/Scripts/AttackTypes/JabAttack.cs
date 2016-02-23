using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JabAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        animator.SetTrigger("Light Jab");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        animator.SetTrigger("Heavy Jab");
        base.PrepareToHeavyAttack();
    }
}