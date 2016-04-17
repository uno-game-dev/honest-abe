using UnityEngine;

class ShootAttack : BaseAttack
{
    public GameObject bulletSpark = null;
	private CharacterState characterState;
    public float aimDuration = 1f;
    public float shootDuration = 1f;
    public float reloadDuration = 1f;

    protected override void PrepareToLightAttack()
    {
        base.PrepareToLightAttack();
        Aim();
    }

    protected override void PrepareToHeavyAttack()
    {
        base.PrepareToHeavyAttack();
        Aim();
    }

    protected override void PerformLightAttack()
    {
        base.PerformLightAttack();
        Shoot();
    }

    protected override void PerformHeavyAttack()
    {
        base.PerformHeavyAttack();
        Shoot();
    }

    protected override void FinishLightAttack()
    {
        base.FinishLightAttack();
        Reload();
    }

    protected override void FinishHeavyAttack()
    {
        base.FinishHeavyAttack();
        Reload();
    }

    private void Aim()
    {
        if (!IsAttacking()) return;

        animator.Play("Aim Musket");
    }

    private void Shoot()
    {
        if (!IsAttacking()) return;

        animator.Play("Shoot Musket");
        ShootCollisionCheck();
    }

    private void Reload()
    {
        if (!IsAttacking()) return;

        animator.Play("Reload Musket");
    }

    private void ShootCollisionCheck()
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
}