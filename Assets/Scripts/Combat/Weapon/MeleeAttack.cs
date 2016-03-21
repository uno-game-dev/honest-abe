using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public enum Hand { Left, Right }
    public Hand _hand;

    protected override void PrepareToLightAttack()
    {
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        AnimationClip clip = animator.GetAnimationClip("Punch");
        if (!clip) clip = animator.GetAnimationClip("LightSwipe");
        //animator.SetFloat("PlaySpeed", clip.length / duration);

        if (_hand == Hand.Left)
        {
            animator.SetBool("LeftHand", true);
            _hand = Hand.Right;
        }
        else
        {
            animator.SetBool("LeftHand", false);
            _hand = Hand.Left;
        }
        animator.SetTrigger("Light Punch");
        base.PrepareToLightAttack();
    }

    protected override void FinishLightAttack()
    {
        base.FinishLightAttack();
    }

    protected override void PrepareToHeavyAttack()
    {
        float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
        AnimationClip clip = animator.GetAnimationClip("Punch");
        if (!clip) clip = animator.GetAnimationClip("HeavySwipe");
        //animator.SetFloat("PlaySpeed", clip.length / duration);

        if (_hand == Hand.Left)
        {
            animator.SetBool("LeftHand", true);
            _hand = Hand.Right;
        }
        else
        {
            animator.SetBool("LeftHand", false);
            _hand = Hand.Left;
        }
        animator.SetTrigger("Heavy Punch");
        base.PrepareToHeavyAttack();
    }

    protected override void FinishHeavyAttack()
    {
        base.FinishHeavyAttack();
    }
}