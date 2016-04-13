using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum State { Null, Light, Heavy, Grab, Throw }
    public enum Hand { Left, Right, Both }

    public State attackState = State.Null;
    public Hand hand = Hand.Right;
    public Weapon weapon;
    public Dictionary<Weapon.AttackType, BaseAttack> attackTypes = new Dictionary<Weapon.AttackType, BaseAttack>();
    public bool emptyHanded = true; // This will be true when Abe has no weapon in his hand and false if he does

    private GameObject _attackBox;
    private Animator _animator;
    private GameObject _leftHand;
    private GameObject _rightHand;
    private BaseAttack _attackType;
    private CharacterState _characterState;
    private ChainAttack _chainAttack;

    private void Start()
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
        _attackBox.transform.SetParent(transform, true);
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

        if (weapon.attackType != Weapon.AttackType.Melee)
            emptyHanded = false;
        else
            emptyHanded = true;

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
            weapon.transform.SetParent(_rightHand.transform, true);
            weapon.transform.localPosition = weapon.heldOffset;
            weapon.transform.localEulerAngles = weapon.heldOrientation;
            weapon.transform.GetChild(0).localRotation = Quaternion.identity;
            weapon.transform.GetChild(0).localPosition = Vector3.zero;
            weapon.gameObject.layer = gameObject.layer;
        }

    }

    public float GetDamageAmount()
    {
        _chainAttack = GetComponent<ChainAttack>();
        float chainAttackDamage = _chainAttack ? _chainAttack.numberOfChainAttacks : 0;
        if (attackState == State.Heavy)
            return weapon.heavyDamage + chainAttackDamage;
        else
            return weapon.lightDamage + chainAttackDamage;
    }

	public float GetStunAmount()
	{
		switch (attackState) {
		case State.Light:
			return weapon.lightStun;
			break;
		case State.Heavy:
			return weapon.heavyStun;
			break;
		case State.Throw :
			return 4f;
			break;
		default:
			return 1f;
			break;
		}
	}

	public float GetKnockbackAmount()
	{
		switch (attackState) {
		case State.Light:
			return weapon.lightKnockback;
			break;
		case State.Heavy:
			return weapon.heavyKnockback;
			break;
		default:
			return 0f;
			break;
		}
	}

    private BaseAttack CreateAttackType(Weapon.AttackType attackType)
    {
        foreach (MonoBehaviour component in GetComponents<MonoBehaviour>())
            if (component is BaseAttack)
                component.enabled = false;

        BaseAttack attack;
		if (attackType == Weapon.AttackType.Melee)
			attack = this.GetOrAddComponent<MeleeAttack> ();
		else if (attackType == Weapon.AttackType.Swing)
			attack = this.GetOrAddComponent<SwingAttack> ();
		else if (attackType == Weapon.AttackType.Jab)
			attack = this.GetOrAddComponent<JabAttack> ();
		else if (attackType == Weapon.AttackType.Shoot)
			attack = this.GetOrAddComponent<ShootAttack> ();
        else if (attackType == Weapon.AttackType.Knife)
            attack = this.GetOrAddComponent<KnifeAttack>();
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

        if (gameObject.tag == "Player") EventHandler.SendEvent(EventHandler.Events.LIGHT_SWING);

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

        if (gameObject.tag == "Player") EventHandler.SendEvent(EventHandler.Events.HEAVY_SWING);

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