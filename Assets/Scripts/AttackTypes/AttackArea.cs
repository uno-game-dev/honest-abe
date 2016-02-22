using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject hit;
    private BaseCollision _collision;
    private ChainAttack _chainAttack;
	private Attack _attack;
    private bool _updateChainAttack;

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        _chainAttack = GetComponentInParent<ChainAttack>();
		_attack = GetComponentInParent<Attack>();
    }

    private void OnEnable()
    {
        _collision.OnCollision += OnCollision;
        _updateChainAttack = true;
    }

    private void OnDisable()
    {
        _collision.OnCollision -= OnCollision;
        if (!hit && _chainAttack)
            _chainAttack.Miss();
        hit = null;
    }

    private void Update()
    {
        _collision.Tick();
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (_updateChainAttack && _chainAttack)
        {
            _chainAttack.Hit();
            _updateChainAttack = false;
        }
		if (transform.parent.gameObject.tag == "Player" && _attack.attackState == Attack.State.Heavy)
		{
			GlobalSettings.executionPerformed = true;
		}
        this.hit = hit.collider.gameObject;
    }
}
