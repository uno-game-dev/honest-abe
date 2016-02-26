using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour
{
    public enum State { Null, Prepare, Perform, Finish }

    public State state;
    private Animator _animator;
    private Attack _attack;
    private float prepareThrowTime = 0.3f;
    private float performThrowTime = 0.3f;
    private float finishThrowTime = 0.3f;
    private CharacterState _characterState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<Attack>();
        _characterState = GetComponent<CharacterState>();
    }

    public void StartThrow()
    {
        if (state != State.Null)
            return;

        if (_attack.weapon.attackType == Weapon.AttackType.Melee)
            return;

        if (!_characterState.CanThrow())
            return;

        PrepareThrow();
    }

    private void PrepareThrow()
    {
        SetState(State.Prepare);
        _characterState.SetState(CharacterState.State.Throw);
        Invoke("PerformThrow", prepareThrowTime);
    }

    private void PerformThrow()
    {
        _attack.weapon.transform.parent = null;
        _attack.weapon.GetComponent<BoxCollider2D>().enabled = true;
        Projectile projectile = _attack.weapon.gameObject.AddComponent<Projectile>();
        SetState(State.Perform);
        _attack.weapon.transform.rotation = Quaternion.identity;
        _attack.SetWeapon(GetComponent<Weapon>());
        _attack.emptyHanded = true;
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
        _characterState.SetState(CharacterState.State.Idle);
    }

    private void SetState(State newState)
    {
        //_animator.SetInteger("Throw", (int)newState);
        state = newState;
    }
}
