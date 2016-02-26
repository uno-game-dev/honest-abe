using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject hit;
    private BaseCollision _collision;
    private ChainAttack _chainAttack;
	private Attack _attack;
    private bool _updateChainAttack;
    private bool alreadyHit = false;

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
            //PerkManager.PerformPerkEffects();
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
        alreadyHit = false;
    }

    private void Update()
    {
        _collision.Tick();
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (transform.parent.gameObject.tag == "Player" && _attack.attackState == Attack.State.Heavy && !alreadyHit) {
            PerkManager.PerformPerkEffects();
            alreadyHit = true;
        }

        if (_updateChainAttack && _chainAttack)
        {
            _chainAttack.Hit();
            _updateChainAttack = false;
        }
        this.hit = hit.collider.gameObject;
    }
}
