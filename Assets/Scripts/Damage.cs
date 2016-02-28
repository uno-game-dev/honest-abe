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

    public void ExecuteDamage(GameObject toObject, float damageAmount, Collider2D collider)
    {
        if (health = toObject.GetComponent<Health>())
            health.Decrease(Convert.ToInt32(damageAmount), damageRate);
        if (collider)
            AddBlood(collider);
    }

    private void OnCollision(Collider2D collider)
    {
        if (collider.tag == "Damage")
        {
            damageAmount = collider.transform.GetComponentInParent<Attack>().GetDamageAmount();
            ExecuteDamage(gameObject, damageAmount, collider);
        }
    }

    private void AddBlood(Collider2D collider)
    {
        Weapon weapon = collider.transform.GetComponentInParent<Attack>().weapon;
        if (weapon.attackType == Weapon.AttackType.Swing)
        {
            if (bloodFountain)
            {
                GameObject blood = Instantiate(bloodFountain);
                blood.transform.position = collider.transform.position;
                Destroy(blood, 10);
            }
        }
        else
        {
            if (bloodSplatter)
            {
                GameObject blood = Instantiate(bloodSplatter);
                blood.transform.localPosition = collider.transform.position;
                Destroy(blood, 10);
            }
        }
    }
}
