using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{
    public enum AttackType { Melee, Swing, Jab, Gun, Projectile }

    public AttackType attackType = AttackType.Melee;
    public Vector2 attackSize = Vector2.one;
    public Vector2 attackOffset = new Vector2(1, 0.5f);
    public Vector3 heldOffset = Vector3.zero;
    public Vector3 heldOrientation = Vector3.zero;
    public float lightDamage = 10;
    public float heavyDamage = 15;

    private BaseCollision _collision;
    private PlayerControls controls;
    public bool justCollided;

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        controls = GameObject.Find("Player").GetComponent<PlayerControls>();
    }

    private void OnEnable()
    {
        if (tag == "Weapon")
            _collision.OnCollision += OnCollision;
    }

    private void OnDisable()
    {
        _collision.OnCollision -= OnCollision;
    }

    private void OnCollision(RaycastHit2D hit)
    {
        Debug.Log(hit.collider.gameObject.name);
        if (!justCollided)
        {
            controls.ResetHold();
            justCollided = true;

        }

        if (controls.heldComplete && justCollided && controls.justClicked)
        {
            hit.collider.gameObject.GetComponent<Attack>().SetWeapon(this);
            OnDisable();
        }
    }
}
