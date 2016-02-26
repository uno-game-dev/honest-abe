using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public enum State { Null, Prepare, Perform, Hold, Finish }

    public GameObject grabArea;
    public float prepareGrabTime = 0.3f;
    public float performGrabTime = 0.3f;
    public float finishGrabTime = 0.3f;
    public State state;

    private Animator _animator;
    private Attack _attack;
    private GameObject _grabbed;
    private float _previousAnimationSpeed;
    private CharacterState _characterState;

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
        grabArea.transform.parent = transform;
        grabArea.transform.localPosition = new Vector3(1f, 0.5f, 0f);
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
        float duration = prepareGrabTime + performGrabTime + finishGrabTime;
        _previousAnimationSpeed = _animator.speed;
        _animator.speed = _animator.GetAnimationClip("Start Grab").length / duration;
        _characterState.SetState(CharacterState.State.Grab);
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
        _animator.speed = _previousAnimationSpeed;

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

        _animator.SetTrigger("Grab Punch");
        Damage();
    }

    private void Damage()
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit.point = _grabbed.transform.position;
        _grabbed.GetComponent<Damage>().ExecuteDamage(_grabbed, 10, hit);
    }

    public void Throw()
    {
        if (state != State.Hold)
            return;

        _animator.SetTrigger("Grab Throw");
        _grabbed.transform.localPosition += new Vector3(5, 0, 0);
        Release();
    }

    public void Release()
    {
        _grabbed.GetComponent<Grabbable>().Release();
        _grabbed = null;
        BackToIdle();
    }

    private void SetState(State newState)
    {
        _animator.SetInteger("Grab", (int)newState);
        state = newState;
    }
}
