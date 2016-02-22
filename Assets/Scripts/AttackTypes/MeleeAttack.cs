using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    protected override void PrepToLightAttack()
    {
        animator.SetTrigger("Light Punch");
        base.PrepToLightAttack();
    }

    protected override void PrepToHeavyAttack()
    {
        animator.SetTrigger("Heavy Punch");
        base.PrepToHeavyAttack();
    }    
}