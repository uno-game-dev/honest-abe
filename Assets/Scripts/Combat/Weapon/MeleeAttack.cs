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
            _hand = Hand.Right;
            if (tag == "Player")
                SoundPlayer.Play("Light Punch Swing");
        }
        else
        {
            animator.Play("Light Attack Melee Right");
            _hand = Hand.Left;
            if (tag == "Player")
                SoundPlayer.Play("Light Punch Swing");
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
            SoundPlayer.Play("Bear Heavy Attack");
        }
        if (_hand == Hand.Left)
        {
            animator.Play("Heavy Attack Melee Left");
            _hand = Hand.Right;
            if (tag == "Player")
                SoundPlayer.Play("Heavy Punch Swing");
        }
        else
        {
            animator.Play("Heavy Attack Melee Right");
            _hand = Hand.Left;
            if (tag == "Player")
                SoundPlayer.Play("Heavy Punch Swing");
        }
        base.PrepareToHeavyAttack();
    }

    protected override void FinishHeavyAttack()
    {
        base.FinishHeavyAttack();
    }
}