using UnityEngine;
using System.Collections;
using System;

public class Jump : MonoBehaviour
{
    public float gravityMultiplier = 1;
    public float jumpStrength = 5;
    public float height = 0;

    [HideInInspector]
    public bool isGrounded;

    private float gravity = -9.81f;
    private float verticalVelocity;
    private float _previousHeight;
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity = jumpStrength;
            isGrounded = false;
        }

        height += verticalVelocity * Time.deltaTime;
        height = Mathf.Clamp(height, 0, height);

        if (verticalVelocity <= 0)
            CheckGrounded();

        _previousHeight = height;

        verticalVelocity += gravity * gravityMultiplier * Time.deltaTime;
        verticalVelocity = height <= 0 ? 0 : verticalVelocity;
    }

    private void CheckGrounded()
    {
        if (_previousHeight == height)
            isGrounded = true;
        else
            isGrounded = false;
    }
}
