using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public enum Hand { Left, Right }
    public Hand _hand;

    protected override void PrepareToLightAttack()
    {
        if (name == "Bear")
        {
            animator.PlayAtSpeed("Light Swipe");
        }
        if (_hand == Hand.Left)
        {
            animator.PlayAtSpeed("Light Attack Melee Left");
            _hand = Hand.Right;
        }
        else
        {
            animator.PlayAtSpeed("Light Attack Melee Right");
            _hand = Hand.Left;
        }
        base.PrepareToLightAttack();
    }

    protected override void FinishLightAttack()
    {
        base.FinishLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        if (name == "Bear")
        {
            animator.PlayAtSpeed("Heavy Swipe");
        }
        if (_hand == Hand.Left)
        {
            animator.PlayAtSpeed("Heavy Attack Melee Left");
            _hand = Hand.Right;
        }
        else
        {
            animator.PlayAtSpeed("Heavy Attack Melee Right");
            _hand = Hand.Left;
        }
        base.PrepareToHeavyAttack();
    }

    protected override void FinishHeavyAttack()
    {
        base.FinishHeavyAttack();
    }
}