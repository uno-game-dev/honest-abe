using UnityEngine;

class ShootAttack : BaseAttack
{
    public GameObject bulletSpark = null;
	private CharacterState characterState;

    protected override void PrepareToLightAttack()
    {
        if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
        base.PrepareToLightAttack();
        animator.Play("Shoot Musket");
    }

    protected override void PrepareToHeavyAttack()
    {
        if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
        base.PrepareToHeavyAttack();
        animator.Play("Reload Musket");
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
		base.BackToIdle();
	}
}