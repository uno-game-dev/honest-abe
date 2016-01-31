using UnityEngine;
using System.Collections;
using System;

public class TakeDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            System.Random r = new System.Random();
            int damage = r.Next(1, 10);
            Debug.Log(string.Format("Take Damage {0}!", damage));
        }
    }
}
