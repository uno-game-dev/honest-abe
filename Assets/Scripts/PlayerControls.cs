using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Attack _attack;
    private Movement _movement;

    private float mouseHeldTime;
    private float timeToConsiderHeld;
    [HideInInspector]
    public bool heldComplete, justClicked;

    void Start()
    {
        _attack = GetComponent<Attack>();
        timeToConsiderHeld = .7f;
        heldComplete = false;
        _movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            justClicked = true;
            _attack.LightAttack();
        }

        if (Input.GetButtonDown("Fire2"))
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

        if (Input.GetButtonDown("Fire3"))
            _attack.Grab();

        if (Input.GetButtonUp("Fire3"))
            _attack.Release();

        if (Input.GetButtonDown("Jump"))
            _movement.Jump();


        if (Input.GetKeyDown(KeyCode.T))
        {
            PreferenceManager.UpdatePerkStatus(GlobalSettings.hat_dtVampirism_name, 0);
            PreferenceManager.UpdatePerkStatus(GlobalSettings.axe_fire_name, 0);
        }
    }

    public void ResetHold()
    {
        justClicked = false;
        heldComplete = false;
        mouseHeldTime = 0;
    }

}
