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
        animator.TransitionPlay("Shoot Musket");
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
        if (tag == "Player") BackToIdle();

        base.FinishLightAttack();
        Reload();
    }

    protected override void FinishHeavyAttack()
    {
        if (tag == "Player") BackToIdle();

        base.FinishHeavyAttack();
        Reload();
    }

    private void Aim()
    {
        if (!IsAttacking()) return;

        animator.TransitionPlay("Aim Musket");
    }

    private void Shoot()
    {
        if (!IsAttacking()) return;

        if (weapon)
            if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
        animator.TransitionPlay("Shoot Musket");
        SoundPlayer.Play("Rifle Fire");
        ShootCollisionCheck();
    }

    private void Reload()
    {
        if (!IsAttacking()) return;

        SoundPlayer.Play("Rifle Reload");
        animator.TransitionPlay("Reload Musket");
    }

    private void ShootCollisionCheck()
    {
        Vector2 direction = Vector2.right;
        if (GetComponent<Movement>())
            if (GetComponent<Movement>().direction == Movement.Direction.Left)
                direction = Vector2.left;
        Vector2 size = new Vector2(1, 1);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, size, 0, direction, 200, _collision.collisionLayer);
        if (hit)
        {
            Damage damage = hit.collider.GetComponent<Damage>();
            Stun stun = hit.collider.GetComponent<Stun>();
            if (damage)
            {
                damage.ExecuteDamage(attack.GetDamageAmount(), null);
                Object blood = Instantiate(damage.bloodShoot, hit.point, Quaternion.identity);
                if (transform.localScale.x < 0) ((GameObject)blood).transform.Rotate(0, 180, 0);
            }
            if (stun)
                stun.GetStunned(stunAmount: 0.7f, power: Stun.Power.Shoot);
            if (bulletSpark)
            {
                GameObject instance = Instantiate(bulletSpark);
                instance.transform.position = new Vector3(hit.point.x, hit.point.y, -35);
                if (transform.localScale.x > 0)
                {
                    Vector3 euler = instance.transform.localEulerAngles;
                    euler.y = 180;
                    instance.transform.localEulerAngles = euler;
                }
                if (!hit.collider.tag.Contains("Obstacle"))
                    instance.transform.FindContainsInChildren("Wood").SetActive(false);
            }
        }
        base.PerformLightAttack();

        //Enable one use weapons for the Player
        if (gameObject.transform.name == "Player")
        {
            Destroy(gameObject.transform.FindContainsInChildren("Musket"));
            if (GetComponent<PlayerMotor>().savedWeapon)
            {
                gameObject.GetComponent<Attack>().SetWeapon(GetComponent<PlayerMotor>().savedWeapon);
                GetComponent<PlayerMotor>().savedWeapon.transform.gameObject.SetActive(true);
            }
            else {
                gameObject.GetComponent<Attack>().SetWeapon(gameObject.GetComponent<Weapon>());
            }
        }
    }
}