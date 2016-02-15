using UnityEngine;
using System.Collections;
using System;

public class Jump : MonoBehaviour
{
    public float jumpStrength = 5;
    public float height = 0;

    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public float jumpVelocity;

    private float _previousHeight;
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpVelocity = jumpStrength;
            isGrounded = false;
        }

        height += jumpVelocity * Time.deltaTime;
        height = Mathf.Clamp(height, 0, height);

        if (jumpVelocity <= 0)
            CheckGrounded();

        _previousHeight = height;

        jumpVelocity += GlobalSettings.gravity * GlobalSettings.gravityMultiplier * Time.deltaTime;
        jumpVelocity = height <= 0 ? 0 : jumpVelocity;
    }

    private void CheckGrounded()
    {
        if (_previousHeight == height)
            isGrounded = true;
        else
            isGrounded = false;
    }
}
