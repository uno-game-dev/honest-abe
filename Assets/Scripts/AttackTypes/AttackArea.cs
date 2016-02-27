using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject hit;
    private BaseCollision _collision;
    private ChainAttack _chainAttack;
	private Attack _attack;
    private bool _updateChainAttack;
    private List<Collider2D> _colliders = new List<Collider2D>();

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        _chainAttack = GetComponentInParent<ChainAttack>();
		_attack = GetComponentInParent<Attack>();
    }

    private void OnEnable()
    {
        if (transform.parent.gameObject.tag == "Player" && _attack.attackState == Attack.State.Heavy)
        {
            GlobalSettings.performingHeavyAttack = true;
        }
        _collision.OnCollisionEnter += OnCollision;
        _updateChainAttack = true;
    }

    private void OnDisable()
    {
        if (transform.parent.gameObject.tag == "Player" && GlobalSettings.performingHeavyAttack)
        {
            GlobalSettings.performingHeavyAttack = false;
        }
        GlobalSettings.performingHeavyAttack = false;
        _collision.OnCollisionEnter -= OnCollision;
        if (!hit && _chainAttack)
            _chainAttack.Miss();
        hit = null;
        _colliders.Clear();
    }

    private void OnCollision(Collider2D collider)
    {
        if (_colliders.Contains(collider))
            return;

        _colliders.Add(collider);

        if (transform.parent.gameObject.tag == "Player" && _attack.attackState == Attack.State.Heavy)
            PerkManager.PerformPerkEffects();

        if (_updateChainAttack && _chainAttack)
        {
            _chainAttack.Hit();
            _updateChainAttack = false;
        }
        this.hit = collider.gameObject;
    }
}
