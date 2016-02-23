using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    protected override void PrepareToLightAttack()
    {
        animator.SetTrigger("Light Punch");
        base.PrepareToLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        animator.SetTrigger("Heavy Punch");
        base.PrepareToHeavyAttack();
    }    
}