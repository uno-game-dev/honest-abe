using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BaseCollision))]
public class TakeDamageWithBaseCollision : MonoBehaviour
{
    private BaseCollision _trigger;

    void OnEnable()
    {
        _trigger = GetComponent<BaseCollision>();
        _trigger.OnCollision += OnCollision;
    }

    void OnDisable()
    {
        _trigger.OnCollision -= OnCollision;
    }

    private void OnCollision(Collider2D otherCollider, RaycastHit2D hit)
    {
        if (otherCollider.tag == "Player")
        {
            System.Random r = new System.Random();
            int damage = r.Next(1, 10);
            Debug.Log(string.Format("Take Damage {0}!", damage));
        }
    }
}
