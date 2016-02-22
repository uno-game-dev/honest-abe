using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Attack : MonoBehaviour
{
    public enum State { Idle, Prep, Light, Heavy, Grab }
    public enum Hand { Left, Right, Both }

    public State attackState = State.Idle;
    public Hand hand = Hand.Right;
    public Weapon weapon;
    public Dictionary<Weapon.AttackType, BaseAttack> attackTypes = new Dictionary<Weapon.AttackType, BaseAttack>();

    private GameObject _attackBox;
    private Animator _animator;
    private GameObject _leftHand;
    private GameObject _rightHand;
    private BaseAttack _attackType;
    private GameObject _grabBox;
    private Grab _grabbed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        CreateOrGetAttackBox();
        CreateOrGetGrabBox();

        _leftHand = this.FindContainsInChildren("LArmPalm");
        _rightHand = this.FindContainsInChildren("RArmPalm");
        if (_leftHand == null) _leftHand = this.FindContainsInChildren("LArmHand");
        if (_rightHand == null) _rightHand = this.FindContainsInChildren("RArmHand");

        Weapon weapon = this.GetOrAddComponent<Weapon>();
        SetWeapon(weapon);
    }

    private void CreateOrGetAttackBox()
    {
        _attackBox = this.FindInChildren("Attack Box");
        if (_attackBox)
            return;

        _attackBox = GameObject.CreatePrimitive(PrimitiveType.Quad); // For Debug Purposes
        // _attackBox = new GameObject(); // Use this one when done debugging
        _attackBox.name = "Attack Box";
        _attackBox.transform.parent = transform;
        _attackBox.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        _attackBox.tag = "Damage";
        _attackBox.layer = gameObject.layer;
        DestroyImmediate(_attackBox.GetComponent<MeshCollider>());
        _attackBox.AddComponent<BoxCollider2D>().isTrigger = true;
        _attackBox.AddComponent<BaseCollision>().collisionLayer = LayerMask.GetMask("Enemy");
        _attackBox.AddComponent<AttackArea>();
        _attackBox.SetActive(false);
    }

    private void CreateOrGetGrabBox()
    {
        _grabBox = this.FindInChildren("Grab Area");
        if (_grabBox)
            return;

        _grabBox = GameObject.CreatePrimitive(PrimitiveType.Quad); // For Debug Purposes
        //_grabBox = new GameObject(); // Use this one when done debugging
        _grabBox.name = "Grab Area";
        _grabBox.transform.parent = transform;
        _grabBox.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        _grabBox.tag = "Grab";
        _grabBox.layer = gameObject.layer;
        DestroyImmediate(_grabBox.GetComponent<MeshCollider>());
        _grabBox.AddComponent<BoxCollider2D>().isTrigger = true;
        _grabBox.AddComponent<BaseCollision>();
        _grabBox.SetActive(false);
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
            weapon.transform.localPosition = weapon.heldOffset;
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

    private BaseAttack CreateAttackType(Weapon.AttackType attackType)
    {
        foreach (MonoBehaviour component in GetComponents<MonoBehaviour>())
            if (component is BaseAttack)
                component.enabled = false;

        BaseAttack attack;
        if (attackType == Weapon.AttackType.Melee)
            attack = this.GetOrAddComponent<MeleeAttack>();
        else if (attackType == Weapon.AttackType.Swing)
            attack = this.GetOrAddComponent<SwingAttack>();
        else if (attackType == Weapon.AttackType.Jab)
            attack = this.GetOrAddComponent<JabAttack>();
        else
            attack = this.GetOrAddComponent<MeleeAttack>();

        attack.Animator = _animator;
        attack.Attack = this;
        attack.AttackArea = _attackBox;
        return attack;
    }

    public void LightAttack()
    {
        _attackType.AddAttack(BaseAttack.AttackStrength.Light);
    }

    public void HeavyAttack()
    {
        _attackType.AddAttack(BaseAttack.AttackStrength.Heavy);
    }

    public void Grab()
    {
        _grabBox.SetActive(true);
        attackState = State.Grab;
    }

    public void FinishGrab(Grab grabbed)
    {
        _grabBox.SetActive(false);
        _grabbed = grabbed;
    }

    public void Release()
    {
        if (attackState != State.Grab)
            return;

        if (_grabbed)
            _grabbed.Release();

        _grabBox.SetActive(false);
        attackState = State.Idle;
    }
}