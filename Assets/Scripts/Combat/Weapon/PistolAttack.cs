using UnityEngine;

class PistolAttack : BaseAttack
{
    public GameObject bulletSpark = null;
    private bool isWoman;

    protected override void PrepareToLightAttack()
    {
        isWoman = name.Contains("Woman");
        base.PrepareToLightAttack();
        Aim();
    }

    protected override void PrepareToHeavyAttack()
    {
        isWoman = name.Contains("Woman");
        base.PrepareToHeavyAttack();
        Aim();
    }

    protected override void PerformLightAttack()
    {
        if (!IsAttacking()) return;

        Shoot();
        state = State.Perform;
        Invoke("FinishLightAttack", lightAttackTime);
    }

    protected override void PerformHeavyAttack()
    {
        if (!IsAttacking()) return;

        Shoot();
        state = State.Perform;
        Invoke("FinishHeavyAttack", lightAttackTime);
    }

    protected override void FinishLightAttack()
    {
        if (!IsAttacking()) return;

        base.FinishLightAttack();
        Reload();
    }

    protected override void FinishHeavyAttack()
    {
        if (!IsAttacking()) return;

        base.FinishHeavyAttack();
        Reload();
    }

    private void Aim()
    {
        if (isWoman)
            animator.Play("Woman Draw Pistol");
        else
            animator.Play("Draw Pistol");
    }

    private void Shoot()
    {
        if (isWoman)
            animator.Play("Woman Shoot Pistol");
        else
            animator.Play("Shoot Pistol");
        SoundPlayer.Play("Pistol Fire");
        if (weapon.GetComponent<MusketFire>()) weapon.GetComponent<MusketFire>().Fire();
        ShootCollisionCheck();
    }

    private void Reload()
    {
        if (isWoman)
            animator.Play("Woman Reload Pistol");
        else
            animator.Play("Reload Pistol");
        SoundPlayer.Play("Pistol Reload");
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