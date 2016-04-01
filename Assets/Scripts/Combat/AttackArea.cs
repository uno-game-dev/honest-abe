﻿using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject hit;
    private BaseCollision _collision;
    private ChainAttack _chainAttack;
    private Attack _attack;
    private bool _updateChainAttack;
    private List<Collider2D> _colliders = new List<Collider2D>();
    private BoxCollider2D _collider;
	private GameObject _player;

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        _collider = GetComponent<BoxCollider2D>();
        _chainAttack = GetComponentInParent<ChainAttack>();
        _attack = GetComponentInParent<Attack>();
		_player = GameObject.Find ("Player");
    }

    private void OnEnable()
	{
		_collision.OnCollisionEnter += OnCollision;
		if (_attack.attackState == Attack.State.Heavy)
			EventHandler.SendEvent(EventHandler.Events.HEAVY_SWING);

		if (_chainAttack && _chainAttack.numberOfChainAttacks == 0 && _attack.attackState == Attack.State.Heavy)
            _updateChainAttack = false;
        else
            _updateChainAttack = true;
	}

    private void OnDisable()
    {
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

		if (transform.parent.gameObject.tag == "Player") {
			if (_attack.attackState == Attack.State.Heavy)
				EventHandler.SendEvent (EventHandler.Events.HEAVY_HIT);
			else if (_attack.attackState == Attack.State.Light)
				EventHandler.SendEvent (EventHandler.Events.LIGHT_HIT);
		} else {
			// Knock the player down if this was a trip attack (i.e. if I'm a bushwhacker and this was a Heavy attack)
			if (collider.name.Contains("Bushwhacker") && _attack.attackState == Attack.State.Heavy) {
				_player.GetComponent<KnockDown> ().StartKnockDown (0);
			}
		}

        if (_updateChainAttack && _chainAttack)
        {
            if (_attack.attackState == Attack.State.Heavy)
                _chainAttack.ShowComboBreak();
            else
                _chainAttack.Hit();
            _updateChainAttack = false;
        }
        this.hit = collider.gameObject;
    }

    public bool IsShootType()
    {
        return _attack.weapon.attackType == Weapon.AttackType.Shoot;
    }
}
