using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Attack : MonoBehaviour
{
    public enum State { Null, Light, Heavy, Grab, Throw }
    public enum Hand { Left, Right, Both }

    public State attackState = State.Null;
    public Hand hand = Hand.Right;
    public Weapon weapon;
    public Dictionary<Weapon.AttackType, BaseAttack> attackTypes = new Dictionary<Weapon.AttackType, BaseAttack>();

    private GameObject _attackBox;
    private Animator _animator;
    private GameObject _leftHand;
    private GameObject _rightHand;
    private BaseAttack _attackType;
    private CharacterState _characterState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterState = GetComponent<CharacterState>();

        CreateOrGetAttackBox();

        _leftHand = this.FindContainsInChildren("LArmPalm");
        _rightHand = this.FindContainsInChildren("RArmPalm");
        if (_leftHand == null) _leftHand = this.FindContainsInChildren("LArmHand");
        if (_rightHand == null) _rightHand = this.FindContainsInChildren("RArmHand");

        if (!weapon) weapon = this.GetOrAddComponent<Weapon>();
        SetWeapon(weapon);
    }

    private void CreateOrGetAttackBox()
    {
        _attackBox = this.FindInChildren("Attack Box");
        if (_attackBox)
            return;

        //_attackBox = GameObject.CreatePrimitive(PrimitiveType.Quad); // For Debug Purposes
        _attackBox = new GameObject(); // Use this one when done debugging
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

    public void SetWeapon(Weapon weapon)//, Hand hand = Hand.Right)
    {
        this.weapon = weapon;

        if (attackTypes.ContainsKey(weapon.attackType))
        {
            _attackType = attackTypes[weapon.attackType];
            _attackType.weapon = weapon;
        }
        else
        {
            _attackType = CreateAttackType(weapon.attackType);
            _attackType.weapon = weapon;
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

        attack.animator = _animator;
        attack.attack = this;
        attack.attackArea = _attackBox;
        return attack;
    }

    public void LightAttack()
    {
        if (attackState != State.Null)
            return;

        if (!_characterState.CanAttack())
            return;

        _characterState.SetState(CharacterState.State.Attack);

        attackState = State.Light;
        _attackType.StartLightAttack();
    }

    public void HeavyAttack()
    {
        if (attackState != State.Null)
            return;

        if (!_characterState.CanAttack())
            return;

        _characterState.SetState(CharacterState.State.Attack);

        attackState = State.Heavy;
        _attackType.StartHeavyAttack();
    }

    public void FinishAttack()
    {
        attackState = State.Null;
        _characterState.SetState(CharacterState.State.Idle);
    }
}