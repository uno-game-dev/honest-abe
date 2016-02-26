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
        _collision.OnCollision += OnCollision;
        _updateChainAttack = true;
    }

    private void OnDisable()
    {
        if (transform.parent.gameObject.tag == "Player" && GlobalSettings.performingHeavyAttack)
        {
            GlobalSettings.performingHeavyAttack = false;
        }
        GlobalSettings.performingHeavyAttack = false;
        _collision.OnCollision -= OnCollision;
        if (!hit && _chainAttack)
            _chainAttack.Miss();
        hit = null;
        _colliders.Clear();
    }

    private void Update()
    {
        _collision.Tick();
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (_colliders.Contains(hit.collider))
            return;

        _colliders.Add(hit.collider);

        if (_updateChainAttack && _chainAttack)
        {
            _chainAttack.Hit();
            _updateChainAttack = false;
        }
        this.hit = hit.collider.gameObject;
    }
}
