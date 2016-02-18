using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    private Attack _attack;
    private Movement _movement;

    void Start()
    {
        _attack = GetComponent<Attack>();
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            _attack.LightAttack();

        if (Input.GetButtonDown("Fire2"))
            _attack.HeavyAttack();

        if (Input.GetButtonDown("Jump"))
            _movement.Jump();
    }
}
