using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public enum Hand { Left, Right }
    public Hand _hand;

    protected override void PrepareToLightAttack()
    {
        if (animator.runtimeAnimatorController.name == "Bear")
        {
            animator.TransitionPlay("Light Swipe");
        }
        if (_hand == Hand.Left)
        {
            animator.TransitionPlay("Light Attack Melee Left");
            _hand = Hand.Right;
            if (tag == "Player")
                SoundPlayer.Play("Light Punch Swing");
        }
        else
        {
            animator.TransitionPlay("Light Attack Melee Right");
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
            animator.TransitionPlay("Heavy Swipe");
            SoundPlayer.Play("Bear Heavy Attack");
        }
        if (_hand == Hand.Left)
        {
            animator.TransitionPlay("Heavy Attack Melee Left");
            _hand = Hand.Right;
            if (tag == "Player")
                SoundPlayer.Play("Heavy Punch Swing");
        }
        else
        {
            animator.TransitionPlay("Heavy Attack Melee Right");
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