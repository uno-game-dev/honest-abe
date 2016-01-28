using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BaseTrigger))]
public class TakeDamage : MonoBehaviour
{
    private BaseTrigger _trigger;

    void OnEnable()
    {
        _trigger = GetComponent<BaseTrigger>();
        _trigger.TriggerEnter += OnCollision;
    }

    void OnDisable()
    {
        _trigger.TriggerEnter -= OnCollision;
    }

    private void OnCollision(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            System.Random r = new System.Random();
            int damage = r.Next(1, 10);
            Debug.Log(string.Format("Take Damage {0}!", damage));
        }
    }
}
