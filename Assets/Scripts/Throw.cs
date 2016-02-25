using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour
{
    public enum State { Null, Prepare, Perform, Finish }

    public State state;
    private Animator _animator;
    private Movement _movement;
    private Attack _attack;
    private float prepareThrowTime = 0.3f;
    private float performThrowTime = 0.3f;
    private float finishThrowTime = 0.3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _attack = GetComponent<Attack>();
    }

    public void StartThrow()
    {
        if (state != State.Null)
            return;

        if (_attack.weapon.attackType == Weapon.AttackType.Melee)
            return;

        if (_movement.state != Movement.State.Idle && _movement.state != Movement.State.Walk)
            return;

        PrepareThrow();
    }

    private void PrepareThrow()
    {
        SetState(State.Prepare);
        _movement.state = Movement.State.Attack;
        Invoke("PerformThrow", prepareThrowTime);
    }

    private void PerformThrow()
    {
        _attack.weapon.transform.parent = null;
        Projectile projectile = _attack.weapon.gameObject.AddComponent<Projectile>();
        SetState(State.Perform);
        _attack.weapon.transform.rotation = Quaternion.identity;
        _attack.SetWeapon(GetComponent<Weapon>());
        Invoke("FinishThrow", performThrowTime);
    }

    private void FinishThrow()
    {
        SetState(State.Finish);
        Invoke("BackToIdle", finishThrowTime);
    }

    private void BackToIdle()
    {
        SetState(State.Null);
        _movement.state = Movement.State.Idle;
    }

    private void SetState(State newState)
    {
        //_animator.SetInteger("Throw", (int)newState);
        state = newState;
    }
}
