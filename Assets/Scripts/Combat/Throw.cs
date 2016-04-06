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
        _animator.SetTrigger("Throw");
        Invoke("PerformThrow", prepareThrowTime);
    }

    private void PerformThrow()
    {
        _attack.weapon.transform.SetParent(null, true);
        _attack.weapon.transform.localScale = Vector3.one;
        _attack.weapon.transform.position = transform.position;
        _attack.weapon.GetComponent<BoxCollider2D>().enabled = true;
        _attack.weapon.GetOrAddComponent<Projectile>().StartProjectile(
			_movement.direction == Movement.Direction.Left ? -velocity : velocity, (int)(_attack.weapon.throwDamage) );
        SetState(State.Perform);
        _attack.weapon.transform.rotation = Quaternion.identity;
        _attack.weapon.gameObject.layer = LayerMask.NameToLayer("Items");
        _attack.SetWeapon(GetComponent<Weapon>());
        _attack.emptyHanded = true;
        Invoke("FinishThrow", performThrowTime);
    }

    private void FinishThrow()
    {
        _attack.emptyHanded = true;
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
