using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float grabDistance = 3;
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

        InputManager.Action mobileAction = InputManager.GetAction();

        if (_grab.state == Grabber.State.Hold)
        {
            if (mobileAction == InputManager.Action.LightAttack || mobileAction == InputManager.Action.PickupOrGrab)
                _grab.Punch();
            else if (mobileAction == InputManager.Action.HeavyAttack)
                _grab.Throw();
            else if (mobileAction == InputManager.Action.Throw)
                _grab.Throw();
            return;
        }
        if (_grab.state != Grabber.State.Null)
            return;

        if (mobileAction == InputManager.Action.LightAttack)
        {
            _attack.LightAttack();
            justClicked = true;
        }

        if (mobileAction == InputManager.Action.HeavyAttack)
            _attack.HeavyAttack();

        if (mobileAction == InputManager.Action.PickupOrGrab)
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                if (Vector2.Distance(transform.position, enemy.transform.position) < grabDistance)
                {
                    _grab.StartGrab();
                    return;
                }

            justClicked = true;
            heldComplete = true;
        }

        if (mobileAction == InputManager.Action.Grab)
            _grab.StartGrab();

        if (mobileAction == InputManager.Action.Jump)
            _jump.StartJump();

        if (mobileAction == InputManager.Action.Throw)
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
