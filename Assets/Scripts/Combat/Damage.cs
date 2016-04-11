using UnityEngine;
using System.Collections;
using System;

public class Damage : MonoBehaviour
{
    public float damageAmount;
    public float damageRate = 0.2f;
    public GameObject bloodSplatter;
    public GameObject bloodSwing;
    public GameObject bloodShoot;
    public GameObject bloodMelee;
    public GameObject bloodJab;

    private Health health;
    private BaseCollision collision;
    private SoundPlayer sound;

    void Awake()
    {
        collision = GetComponent<BaseCollision>();
        sound = GetComponent<SoundPlayer>();
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
			;
        if (tag == "Boss")
			if (GetComponent<Boss>() != null && GetComponent<Boss>().bossName == "Bear")
              EventHandler.SendEvent(EventHandler.Events.BEAR_HIT, GameObject.Find("Player"));
        if (collider)
            AddBlood(collider);
        if (health = GetComponent<Health>())
            health.Decrease(Convert.ToInt32(damageAmount));
    }

    private void OnCollision(Collider2D collider)
    {
        AttackArea attackArea = collider.GetComponent<AttackArea>();
        if (attackArea && attackArea.IsShootType())
            return;

        if (collider.tag == "Damage")
        {
            damageAmount = collider.transform.GetComponentInParent<Attack>().GetDamageAmount();
            SoundPlayer.Play("Damage React Light");
            ExecuteDamage(damageAmount, collider);
        }
    }

    private void AddBlood(Collider2D collider)
    {
        if (collider.GetComponentInParent<Attack>())
            if (collider.GetComponentInParent<Attack>().weapon)
            {
                Weapon.AttackType attackType = collider.GetComponentInParent<Attack>().weapon.attackType;
                if (attackType == Weapon.AttackType.Swing)
                    InstantiateBlood(bloodSwing);
                else if (attackType == Weapon.AttackType.Shoot)
                    InstantiateBlood(bloodShoot);
                else if (attackType == Weapon.AttackType.Melee)
                    InstantiateBlood(bloodMelee);
                else if (attackType == Weapon.AttackType.Jab)
                    InstantiateBlood(bloodJab);
                    return;
            }

        InstantiateBlood(bloodSplatter);
			
    }

    private void InstantiateBlood(GameObject bloodPrefab)
    {
        if (!bloodPrefab)
            return;

        GameObject blood = Instantiate(bloodPrefab);
        blood.transform.position = transform.position;
        Destroy(blood, 10);
    }
}
