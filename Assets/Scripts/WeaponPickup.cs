using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BaseCollision))]
[RequireComponent(typeof(Weapon))]
public class WeaponPickup : MonoBehaviour
{
    private BaseCollision _baseCollision;
    private Weapon _weapon;

    void Awake()
    {
        _baseCollision = GetComponent<BaseCollision>();
        _weapon = GetComponent<Weapon>();
    }

    void OnEnable()
    {
        _baseCollision.OnCollision += OnCollision;
    }

    void OnDisable()
    {
        _baseCollision.OnCollision -= OnCollision;
    }

    void Update()
    {
        _baseCollision.Tick();
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Player")
        {
            hit.collider.GetComponent<Attack>().SetWeapon(_weapon);
            OnDisable();
        }
    }
}
