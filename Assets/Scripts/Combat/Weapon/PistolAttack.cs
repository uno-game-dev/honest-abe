using UnityEngine;

class PistolAttack : BaseAttack
{
    public GameObject bulletSpark = null;

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
        Shoot();
        state = State.Perform;
        Invoke("FinishLightAttack", lightAttackTime);
    }

    protected override void PerformHeavyAttack()
    {
        Shoot();
        state = State.Perform;
        Invoke("FinishHeavyAttack", lightAttackTime);
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
        animator.Play("Draw Pistol");
    }

    private void Shoot()
    {
        animator.Play("Shoot Pistol");
        SoundPlayer.Play("Pistol Fire");
        if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
        ShootCollisionCheck();
    }

    private void Reload()
    {
        SoundPlayer.Play("Pistol Reload");
        animator.Play("Reload Pistol");
    }

    private void ShootCollisionCheck()
    {
        Vector2 direction = Vector2.right;
        if (GetComponent<Movement>())
            if (GetComponent<Movement>().direction == Movement.Direction.Left)
                direction = Vector2.left;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, direction, 200, _collision.collisionLayer);
        if (hit)
        {
            Damage damage = hit.collider.GetComponent<Damage>();
            Stun stun = hit.collider.GetComponent<Stun>();
            if (damage)
                damage.ExecuteDamage(attack.GetDamageAmount(), hit.collider);
            if (stun)
                stun.GetStunned();
            if (bulletSpark)
                Instantiate(bulletSpark, hit.point, Quaternion.identity);
        }
    }
}