using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Attack : MonoBehaviour
{
    public enum State { Idle, Prep, Light, Heavy }
    public enum Hand { Left, Right, Both }

    public State attackState = State.Idle;
    public Hand hand = Hand.Right;
    public Weapon weapon;
    public Dictionary<Weapon.AttackType, IAttackType> attackTypes = new Dictionary<Weapon.AttackType, IAttackType>();

    private GameObject _attackBox;
    private Animator _animator;
    private GameObject _leftHand;
    private GameObject _rightHand;
    private IAttackType _attackType;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _attackBox = this.FindInChildren("Melee Area");
        if (!_attackBox)
            CreateAttackBox();

        _leftHand = this.FindContainsInChildren("LArmPalm");
        _rightHand = this.FindContainsInChildren("RArmPalm");
        if (_leftHand == null) _leftHand = this.FindContainsInChildren("LArmHand");
        if (_rightHand == null) _rightHand = this.FindContainsInChildren("RArmHand");

        Weapon weapon = this.GetOrAddComponent<Weapon>();
        SetWeapon(weapon);
    }

    private void CreateAttackBox()
    {
        _attackBox = GameObject.CreatePrimitive(PrimitiveType.Quad); // For Debug Purposes
        //GameObject newMeleeArea = new GameObject(); // Use this one when done debugging
        _attackBox.name = "Melee Area";
        _attackBox.transform.parent = transform;
        _attackBox.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        _attackBox.tag = "Damage";
        _attackBox.layer = gameObject.layer;
        DestroyImmediate(_attackBox.GetComponent<MeshCollider>());
        _attackBox.AddComponent<BoxCollider2D>().isTrigger = true;
        _attackBox.AddComponent<BaseCollision>();
        _attackBox.SetActive(false);
    }

    public void SetWeapon(Weapon weapon)//, Hand hand = Hand.Right)
    {
        this.weapon = weapon;

        if (attackTypes.ContainsKey(weapon.attackType))
        {
            _attackType = attackTypes[weapon.attackType];
            _attackType.Weapon = weapon;
        }
        else
        {
            _attackType = CreateAttackType(weapon.attackType);
            _attackType.Weapon = weapon;
            attackTypes[weapon.attackType] = _attackType;
        }

        if (weapon.attackType != Weapon.AttackType.Melee)
        {
            weapon.transform.parent = _rightHand.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = weapon.heldOrientation;
        }
    }

    public float GetDamageAmount()
    {
        if (attackState == State.Heavy)
            return weapon.heavyDamage;
        else
            return weapon.lightDamage;
    }

    private IAttackType CreateAttackType(Weapon.AttackType attackType)
    {
        foreach (MonoBehaviour component in GetComponents<MonoBehaviour>())
            if (component is IAttackType)
                component.enabled = false;

        if (attackType == Weapon.AttackType.Melee)
        {
            MeleeAttack ma = this.GetOrAddComponent<MeleeAttack>();
            ma.animator = _animator;
            ma.attack = this;
            ma.meleeArea = _attackBox;
            ma.enabled = true;
            return ma;
        }
        if (attackType == Weapon.AttackType.Swing)
        {
            SwingAttack sa = this.GetOrAddComponent<SwingAttack>();
            sa.animator = _animator;
            sa.attack = this;
            sa.meleeArea = _attackBox;
            sa.enabled = true;
            return sa;
        }
        else
        {
            MeleeAttack ma = this.GetOrAddComponent<MeleeAttack>();
            ma.animator = _animator;
            ma.attack = this;
            ma.meleeArea = _attackBox;
            ma.enabled = true;
            return ma;
        }
    }

    public void LightAttack()
    {
        if (attackState != State.Idle)
            return;

        _attackType.LightAttack();
    }

    public void HeavyAttack()
    {
        if (attackState != State.Idle)
            return;

        _attackType.HeavyAttack();
    }
}