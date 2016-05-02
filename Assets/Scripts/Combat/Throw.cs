using UnityEngine;

public class Throw : MonoBehaviour
{
    public enum State { Null, Prepare, Perform, Finish }

    public State state;
    public float velocity = 30;
    private Animator _animator;
    private Attack _attack;
    private float prepareThrowTime = 0.3f;
    private float performThrowTime = 0.3f;
    private float finishThrowTime = 0.3f;
    private CharacterState _characterState;
    private Movement _movement;
	private bool throwOneUseWeapon = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<Attack>();
        _characterState = GetComponent<CharacterState>();
        _movement = GetComponent<Movement>();
    }

    public void StartThrow()
    {
        if (state != State.Null)
            return;

        if (_attack.weapon.attackType == Weapon.AttackType.Melee)
            return;

        if (!_characterState.CanThrow())
            return;

		EventHandler.SendEvent(EventHandler.Events.WEAPON_THROW, _attack.weapon.gameObject);
        PrepareThrow();
    }

    private void PrepareThrow()
    {
        SetState(State.Prepare);
        _characterState.SetState(CharacterState.State.Throw);
        _animator.TransitionPlay("Throw");
        Invoke("PerformThrow", prepareThrowTime);
    }

    private void PerformThrow()
    {
        SoundPlayer.Play("Axe Throw");
        _attack.weapon.transform.SetParent(null, true);
        if (PerkManager.activeAxePerk != null && PerkManager.activeAxePerk.type == Perk.PerkType.AXE_BFA)
            _attack.weapon.transform.localScale = new Vector3(2, 2, 1);
        else
            _attack.weapon.transform.localScale = Vector3.one;
        _attack.weapon.transform.position = transform.position;
        _attack.weapon.GetComponent<BoxCollider2D>().enabled = true;
        _attack.weapon.GetOrAddComponent<Projectile>().StartProjectile(
			_movement.direction == Movement.Direction.Left ? -velocity : velocity, (int)(_attack.weapon.throwDamage) );
        SetState(State.Perform);
        _attack.weapon.transform.rotation = Quaternion.identity;
        _attack.weapon.gameObject.layer = LayerMask.NameToLayer("Items");
		if (_attack.weapon.tag == "OneUseWeapon") {
			_attack.SetWeapon (GameObject.Find ("Player").GetComponent<PlayerMotor> ().savedWeapon);
			GetComponent<PlayerMotor> ().savedWeapon.transform.gameObject.SetActive (true);
			throwOneUseWeapon = true;
		} else {
			_attack.SetWeapon (GetComponent<Weapon> ());
			_attack.emptyHanded = true;
			throwOneUseWeapon = false;
		}
        Invoke("FinishThrow", performThrowTime);
    }

    private void FinishThrow()
    {
		if (!throwOneUseWeapon) {
			_attack.emptyHanded = true;
		}
        SetState(State.Finish);
        Invoke("BackToIdle", finishThrowTime);
    }

    private void BackToIdle()
    {
        SetState(State.Null);
        _characterState.SetState(CharacterState.State.Idle);
    }

    private void SetState(State newState)
    {
        //_animator.SetInteger("Throw", (int)newState);
        state = newState;
    }
}
