using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Attack _attack;

    private float mouseHeldTime;
    private float timeToConsiderHeld;
    [HideInInspector] public bool heldComplete, justClicked;

    void Start()
    {
        _attack = GetComponent<Attack>();
        timeToConsiderHeld = .7f;
        heldComplete = false;
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
    }

    public void ResetHold()
    {
        justClicked = false;
        heldComplete = false;
        mouseHeldTime = 0;
    }

}
