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
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        _collider = GetComponent<BoxCollider2D>();
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

        if (_chainAttack && _chainAttack.numberOfChainAttacks == 0 && _attack.attackState == Attack.State.Heavy)
            _updateChainAttack = false;
        else
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
            PerkManager.PerformPerkEffects(Perk.PerkCategory.AXE);

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
