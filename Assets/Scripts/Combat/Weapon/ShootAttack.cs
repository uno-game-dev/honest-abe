using UnityEngine;

class ShootAttack : BaseAttack
{
    public GameObject bulletSpark = null;
	private CharacterState characterState;

    protected override void PrepareToLightAttack()
    {
        float duration = prepLightAttackTime + lightAttackTime + finishLightAttackTime;
        animator.SetFloat("PlaySpeed", duration);
        animator.SetTrigger("Light Shoot");
        base.PrepareToLightAttack();

        //ALPHA ONLY
        AudioManager.instance.PlayAttackSound(1, 0);
    }

    protected override void PrepareToHeavyAttack()
    {
        float duration = prepHeavyAttackTime + heavyAttackTime + finishHeavyAttackTime;
        animator.SetFloat("PlaySpeed", duration);
        animator.SetTrigger("Heavy Shoot");
        base.PrepareToHeavyAttack();

        //ALPHA ONLY
        AudioManager.instance.PlayAttackSound(1, 0);
    }

    protected override void PerformLightAttack()
    {
        Vector2 direction = Vector2.right;
        if (GetComponent<Movement>())
            if (GetComponent<Movement>().direction == Movement.Direction.Left)
                direction = Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 200, _collision.collisionLayer);
		if (hit) {
			Damage damage = hit.collider.GetComponent<Damage> ();
			Stun stun = hit.collider.GetComponent<Stun> ();
			if (damage)
				damage.ExecuteDamage (attack.GetDamageAmount (), hit.collider);
			if (stun)
				stun.GetStunned ();
			if (bulletSpark)
				Instantiate (bulletSpark, hit.point, Quaternion.identity);
		}
		base.PerformLightAttack ();
    }

    protected override void BackToIdle()
    {
        //characterState = GetComponent<CharacterState>();
		//characterState.SetState(CharacterState.State.Idle);
		base.BackToIdle();

	}
}