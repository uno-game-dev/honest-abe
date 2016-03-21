using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Attack _attack;
    private Movement _movement;
    private Jump _jump;
    private Grabber _grab;
    private float mouseHeldTime;
    private float timeToConsiderHeld;
    private Throw _throw;

    [HideInInspector]
    public bool heldComplete, justClicked;

    void Start()
    {
        _attack = GetComponent<Attack>();
        timeToConsiderHeld = .7f;
        heldComplete = false;
        _movement = GetComponent<Movement>();
        _jump = GetComponent<Jump>();
        _grab = GetComponent<Grabber>();
        _throw = GetComponent<Throw>();
    }

    void Update()
    {
        if (!UIManager.updateActive) return;

        MobileInput.Action mobileAction = MobileInput.GetAction();

        if (mobileAction == MobileInput.Action.LightAttack)
        {
            if (_grab.state == Grabber.State.Hold)
                _grab.Punch();
            else
            {
                justClicked = true;
                _attack.LightAttack();
            }
        }

        if (mobileAction == MobileInput.Action.HeavyAttack)
            if (_grab.state == Grabber.State.Hold)
                _grab.Throw();
            else
                _attack.HeavyAttack();

        if (Input.GetButton("Fire1") && !heldComplete && justClicked)
        {
            mouseHeldTime += Time.deltaTime;

            if (mouseHeldTime >= timeToConsiderHeld)
            {
                mouseHeldTime = 0;
                heldComplete = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            ResetHold();
        }

        if (mobileAction == MobileInput.Action.Grab)
            _grab.StartGrab();

        if (mobileAction == MobileInput.Action.Jump)
            _jump.StartJump();

        if (mobileAction == MobileInput.Action.Throw)
            _throw.StartThrow();
    }

    public void ResetHold()
    {
        justClicked = false;
        heldComplete = false;
        mouseHeldTime = 0;
    }

}
