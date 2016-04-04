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

        if (_grab.state == Grabber.State.Hold)
        {
            if (mobileAction == MobileInput.Action.LightAttack || mobileAction == MobileInput.Action.Pickup)
                _grab.Punch();
            else if (mobileAction == MobileInput.Action.HeavyAttack)
                _grab.Throw();
            else if (mobileAction == MobileInput.Action.Throw)
                _grab.Throw();
            return;
        }

        if (mobileAction == MobileInput.Action.LightAttack)
        {
            _attack.LightAttack();
            justClicked = true;
        }

        if (mobileAction == MobileInput.Action.HeavyAttack)
            _attack.HeavyAttack();

        if (mobileAction == MobileInput.Action.Pickup)
        {
            justClicked = true;
            heldComplete = true;
        }

        if (mobileAction == MobileInput.Action.Grab)
            _grab.StartGrab();

        if (mobileAction == MobileInput.Action.Jump)
            _jump.StartJump();

        if (mobileAction == MobileInput.Action.Throw)
            _throw.StartThrow();
            
        if(Input.GetKeyDown(KeyCode.R)){
			PerkManager.PerformPerkEffects (Perk.PerkCategory.TRINKET);
		}
    }

    public void ResetHold()
    {
        justClicked = false;
        heldComplete = false;
        mouseHeldTime = 0;
    }

}
