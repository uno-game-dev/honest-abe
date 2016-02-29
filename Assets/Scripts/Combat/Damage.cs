using UnityEngine;
using System.Collections;
using System;

public class Damage : MonoBehaviour
{
    public float damageAmount;
    public float damageRate = 0.2f;
    public GameObject bloodSplatter;
    public GameObject bloodFountain;

    private Health health;
    private BaseCollision collision;

    void Awake()
    {
        collision = GetComponent<BaseCollision>();
    }

    void OnEnable()
    {
        collision.OnCollisionEnter += OnCollision;
    }

    void OnDisable()
    {
        collision.OnCollisionEnter -= OnCollision;
    }

    public void ExecuteDamage(float damageAmount, Collider2D collider)
    {
        if (tag == "Enemy")
            AudioManager.instance.PlayDamageSound(new System.Random().Next(2, 5));
        if (tag == "Boss")
            if (UnityEngine.Random.value > 0.5)
                AudioManager.instance.PlayBossSound(0);
            else
                AudioManager.instance.PlayBossSound(1);
        if (collider)
            AddBlood(collider);
        if (health = GetComponent<Health>())
            health.Decrease(Convert.ToInt32(damageAmount));
    }

    private void OnCollision(Collider2D collider)
    {
        if (collider.tag == "Damage")
        {
            damageAmount = collider.transform.GetComponentInParent<Attack>().GetDamageAmount();
            ExecuteDamage(damageAmount, collider);
        }
    }

    private void AddBlood(Collider2D collider)
    {
        if (collider.GetComponentInParent<Attack>())
            if (collider.GetComponentInParent<Attack>().weapon)
                if (collider.GetComponentInParent<Attack>().weapon.attackType == Weapon.AttackType.Swing)
                {
                    //ALPHA ONLY
                    AudioManager.instance.PlayDamageSound();

                    if (bloodFountain)
                    {
                        GameObject blood = Instantiate(bloodFountain);
                        blood.transform.position = collider.transform.position;
                        Destroy(blood, 10);
                    }

                    return;
                }

        //ALPHA ONLY
        AudioManager.instance.PlayDamageSound();
        if (bloodSplatter)
        {
            GameObject blood = Instantiate(bloodSplatter);
            blood.transform.localPosition = collider.transform.position;
            Destroy(blood, 10);
        }
    }
}
