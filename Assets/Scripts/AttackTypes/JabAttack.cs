using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JabAttack : BaseAttack
{
    protected override void PrepToLightAttack()
    {
        animator.SetTrigger("Light Jab");
        base.PrepToLightAttack();
    }

    protected override void PrepToHeavyAttack()
    {
        animator.SetTrigger("Heavy Jab");
        base.PrepToHeavyAttack();
    }
}