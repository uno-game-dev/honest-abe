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
        if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
        base.PrepareToLightAttack();
        Aim();
    }

    protected override void PrepareToHeavyAttack()
    {
        if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
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
		Vector2 size = new Vector2(1, 1);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, size, 0, direction, 200, _collision.collisionLayer);
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

		//Enable one use weapons for the Player
		if(gameObject.transform.name == "Player"){
			Destroy (gameObject.transform.FindContainsInChildren ("Musket"));
			if (GetComponent<PlayerMotor> ().savedWeapon) {
				gameObject.GetComponent<Attack> ().SetWeapon (GetComponent<PlayerMotor> ().savedWeapon);
				GetComponent<PlayerMotor> ().savedWeapon.transform.gameObject.SetActive (true);
			} else {
				gameObject.GetComponent<Attack> ().SetWeapon (new Weapon());
			}
		}
    }
}