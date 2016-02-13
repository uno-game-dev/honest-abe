using UnityEngine;
using System.Collections;
using System;

public class Attack : MonoBehaviour
{
    public enum AttackType { Melee, SwingWeapon, JabWeapon, TwoHandedSwingWeapon, Pistol, Rifle }
    public enum State { Idle, Prep, Light, Heavy }

    public State attackState = State.Idle;
    public AttackType attackType = AttackType.Melee;
    public IAttackType weapon;

    private GameObject _meleeArea;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _meleeArea = this.FindInChildren("Melee Area");
        if (!_meleeArea)
            CreateNewMeleeArea();

        SetAttackType(attackType);
    }

    private void CreateNewMeleeArea()
    {
        _meleeArea = GameObject.CreatePrimitive(PrimitiveType.Quad); // For Debug Purposes
        //GameObject newMeleeArea = new GameObject(); // Use this one when done debugging
        _meleeArea.name = "Melee Area";
        _meleeArea.transform.parent = transform;
        _meleeArea.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        _meleeArea.SetActive(false);
    }

    public void SetAttackType(AttackType newAttackType)
    {
        MeleeAttack meleeAttack = this.FindComponent<MeleeAttack>();
        if (!meleeAttack)
            meleeAttack = gameObject.AddComponent<MeleeAttack>();

        meleeAttack.attack = this;
        meleeAttack.animator = _animator;
        meleeAttack.meleeArea = _meleeArea;

        weapon = meleeAttack;
    }

    public void LightAttack()
    {
        if (attackState != State.Idle)
            return;

        weapon.LightAttack();
    }

    public void HeavyAttack()
    {
        if (attackState != State.Idle)
            return;

        weapon.HeavyAttack();
    }
}