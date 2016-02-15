using UnityEngine;
using System.Collections;
using System;

public class Damage : MonoBehaviour
{
    public float damageAmount;
    public float damageRate = 1f;
    public GameObject bloodSplatter;
    public GameObject bloodFountain;

    private Health health;
    private BaseCollision collision;
    private bool canAddBlood;
    private float bloodTimer;
    private float bloodRate = 0.2f;

    void Start()
    {
        collision = GetComponent<BaseCollision>();
        collision.OnCollision += OnCollision;
    }

    void Update()
    {
        collision.Tick();

        if (!canAddBlood)
            if (bloodTimer > bloodRate)
                canAddBlood = transform;
            else
                bloodTimer += Time.deltaTime;
    }

    public void ExecuteDamage(GameObject toObject)
    {
        if (!health)
            health = toObject.GetComponent<Health>();
        if (health)
            health.Decrease(Convert.ToInt32(damageAmount), damageRate);
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Damage")
        {
            damageAmount = hit.transform.GetComponentInParent<Attack>().GetDamageAmount();
            ExecuteDamage(gameObject);
            AddBlood(hit);
            Debug.Log("Hit! " + gameObject.name);
        }
    }

    private void AddBlood(RaycastHit2D hit)
    {
        if (!canAddBlood)
            return;

        bloodTimer = 0;
        canAddBlood = false;

        Weapon weapon = hit.transform.GetComponentInParent<Attack>().weapon;
        if (weapon.attackType == Weapon.AttackType.Swing)
        {
            if (bloodFountain)
            {
                GameObject blood = Instantiate(bloodFountain);
                blood.transform.localPosition = hit.point;
                Destroy(blood, 10);
            }
        }
        else
        {
            if (bloodSplatter)
            {
                GameObject blood = Instantiate(bloodSplatter);
                blood.transform.localPosition = hit.point;
                Destroy(blood, 10);
            }
        }
    }
}
