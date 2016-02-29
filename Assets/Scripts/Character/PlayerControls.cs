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

        if (Input.GetButtonDown("Fire1"))
        {
            if (_grab.state == Grabber.State.Hold)
                _grab.Punch();
            else
            {
                justClicked = true;
                _attack.LightAttack();
            }
        }

        if (Input.GetButtonDown("Fire2"))
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

        if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.F))
            _grab.StartGrab();

        if (Input.GetButtonDown("Jump"))
            _jump.StartJump();

        if (Input.GetKeyDown(KeyCode.E))
            _throw.StartThrow();
    }

    public void ResetHold()
    {
        justClicked = false;
        heldComplete = false;
        mouseHeldTime = 0;
    }

}
