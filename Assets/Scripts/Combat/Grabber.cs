using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public enum State { Null, Prepare, Perform, Hold, Punch, Throw, Lose, Finish }

    public GameObject grabArea;
    public float prepareGrabTime = 0.3f;
    public float performGrabTime = 0.3f;
    public float finishGrabTime = 0.3f;
    public float grabPunchTime = 0.5f;
    public float grabThrowTime = 0.5f;
    public float grabLoseTime = 0.5f;
    public float grabThrowDamage = 25f;
    public State state;

    private Animator _animator;
    private Attack _attack;
    private GameObject _grabbed;
    private CharacterState _characterState;
    private Weapon _weapon;
    private int _weaponDamage;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<Attack>();
        _characterState = GetComponent<CharacterState>();
        CreateOrGetGrabBox();
    }

    private void CreateOrGetGrabBox()
    {
        grabArea = this.FindInChildren("Grab Area");
        if (grabArea)
            return;

        //grabArea = GameObject.CreatePrimitive(PrimitiveType.Quad); // For Debug Purposes
        grabArea = new GameObject(); // Use this one when done debugging
        grabArea.name = "Grab Area";
        grabArea.transform.SetParent(transform);
        grabArea.transform.localPosition = new Vector3(1, 0, 0);
        grabArea.tag = "Grab";
        grabArea.layer = gameObject.layer;
        DestroyImmediate(grabArea.GetComponent<MeshCollider>());
        grabArea.AddComponent<BoxCollider2D>().isTrigger = true;
        grabArea.AddComponent<BaseCollision>().collisionLayer = LayerMask.GetMask("Enemy");
        grabArea.SetActive(false);
    }

    public void StartGrab()
    {
        if (state == State.Hold)
            Release();
        else if (state != State.Null)
            return;
        else if (!_characterState.CanGrab())
            return;
        else
            PrepareToGrab();
    }

    private void PrepareToGrab()
    {
        _animator.PlayAtSpeed("Grab Idle");
        SetState(State.Prepare);
        Invoke("PerformGrab", prepareGrabTime);
    }

    private void PerformGrab()
    {
        SetState(State.Perform);
        grabArea.SetActive(true);
        Invoke("FinishGrab", performGrabTime);
    }

    private void FinishGrab()
    {
        if (state != State.Perform)
            return;

        SetState(State.Finish);
        grabArea.SetActive(false);
        Invoke("BackToIdle", finishGrabTime);
    }

    private void BackToIdle()
    {
        SetState(State.Null);
        _characterState.SetState(CharacterState.State.Idle);
    }

    public void Hold(GameObject grabbed)
    {
        if (state != State.Perform)
            return;

        SetState(State.Hold);
        grabArea.SetActive(false);
        _grabbed = grabbed;
    }

    public void Punch()
    {
        if (state != State.Hold)
            return;

        state = State.Punch;
        _animator.PlayAtSpeed("Grab Punch");
        Damage();
        Invoke("FinishPunch", grabPunchTime);
    }

    private void FinishPunch()
    {
        if (state != State.Punch)
            return;

        state = State.Hold;
    }

    private void Damage()
    {
        if (!_grabbed)
            return;

        Damage damage = _grabbed.GetComponent<Damage>();
        if (damage)
        {
            _weaponDamage = (int)_attack.GetComponent<Weapon>().lightDamage;
            damage.ExecuteDamage(_weaponDamage, GetComponent<Collider2D>());
        }
    }

    public void Throw()
    {
        if (state != State.Hold)
            return;

        EventHandler.SendEvent(EventHandler.Events.ENEMY_THROW);

        _animator.PlayAtSpeed("Grab Throw");

        _grabbed.GetComponent<Grabbable>().Throw();

        if (_grabbed.GetComponent<Damage>())
            _grabbed.GetComponent<Damage>().ExecuteDamage(grabThrowDamage, GetComponent<Collider2D>());
        _grabbed = null;
        Invoke("BackToIdle", grabThrowTime);
    }

    public void Release()
    {
        if (_grabbed)
            if (_grabbed.GetComponent<Grabbable>())
                _grabbed.GetComponent<Grabbable>().Release();
        _grabbed = null;
        BackToIdle();
    }

    private void SetState(State newState)
    {
        state = newState;
    }
}
