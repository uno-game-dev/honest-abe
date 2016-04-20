using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public enum Hand { Left, Right }
    public Hand _hand;

    protected override void PrepareToLightAttack()
    {
        if (animator.runtimeAnimatorController.name == "Bear")
        {
            animator.Play("Light Swipe");
        }
        if (_hand == Hand.Left)
        {
            animator.Play("Light Attack Melee Left");
            SoundPlayer.Play("Light Punch Swing");
            _hand = Hand.Right;
        }
        else
        {
            animator.Play("Light Attack Melee Right");
            SoundPlayer.Play("Light Punch Swing");
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
        if (animator.runtimeAnimatorController.name == "Bear")
        {
            animator.Play("Heavy Swipe");
        }
        if (_hand == Hand.Left)
        {
            animator.Play("Heavy Attack Melee Left");
            SoundPlayer.Play("Heavy Punch Swing");
            _hand = Hand.Right;
        }
        else
        {
            animator.Play("Heavy Attack Melee Right");
            SoundPlayer.Play("Heavy Punch Swing");
            _hand = Hand.Left;
        }
        base.PrepareToHeavyAttack();
    }

    protected override void FinishHeavyAttack()
    {
        base.FinishHeavyAttack();
    }
}