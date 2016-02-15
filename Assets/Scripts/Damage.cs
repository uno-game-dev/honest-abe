using UnityEngine;
using System.Collections;
using System;

public class Damage : MonoBehaviour
{

    public float damageAmount;
    public float damageRate = 1f;
    private Health health;
    private BaseCollision collision;

    void Start()
    {
        collision = GetComponent<BaseCollision>();
        collision.OnCollision += OnCollision;
    }

    void Update()
    {
        collision.Tick();
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
            Debug.Log("Hit! " + gameObject.name);
        }
    }

}
