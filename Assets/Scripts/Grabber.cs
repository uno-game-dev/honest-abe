using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public enum State { Null, Prepare, Perform, Hold, Finish }

    private Animator _animator;
    private Attack _attack;
    private GameObject _grabbed;
    public State state;
    public GameObject grabArea;
    public float prepareGrabTime = 0.3f;
    public float performGrabTime = 0.3f;
    public float finishGrabTime = 0.3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<Attack>();
    }

    public void StartGrab()
    {
        _animator.SetTrigger("Grab");
        PrepareToGrab();
    }

    private void PrepareToGrab()
    {
        state = State.Prepare;
        Invoke("PerformGrab", prepareGrabTime);
    }

    private void PerformGrab()
    {
        state = State.Perform;
        grabArea.SetActive(true);
        Invoke("FinishGrab", performGrabTime);
    }

    private void FinishGrab()
    {
        if (state != State.Perform)
            return;

        state = State.Finish;
        grabArea.SetActive(false);
        Invoke("BackToIdle", finishGrabTime);
    }

    private void BackToIdle()
    {
        _attack.Release();
        state = State.Null;
    }

    public void Hold(GameObject grabbed)
    {
        if (state != State.Perform)
            return;

        state = State.Hold;
        grabArea.SetActive(false);
        _animator.SetBool("Hold", true);
        _grabbed = grabbed;
    }

    public void Release()
    {
        _grabbed.GetComponent<Grabbable>().Release();
        _animator.SetBool("Hold", false);
        _grabbed = null;
        BackToIdle();
    }
}
