using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Attack _attack;

    private float mouseHeldTime;
    private float timeToConsiderHeld;
    [HideInInspector] public bool heldComplete;

    void Start()
    {
        _attack = GetComponent<Attack>();
        timeToConsiderHeld = 1;
        heldComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _attack.LightAttack();
        }

        if (Input.GetButtonDown("Fire2"))
            _attack.HeavyAttack();

        if (Input.GetButton("Fire1") && !heldComplete)
        {
            mouseHeldTime += Time.deltaTime;

            if (mouseHeldTime >= timeToConsiderHeld)
            {
                mouseHeldTime = 0;
                Debug.Log("Left mouse was held");
                heldComplete = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
            heldComplete = false;
    }

}
