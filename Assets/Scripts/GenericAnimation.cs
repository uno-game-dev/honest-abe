using UnityEngine;
using System.Collections;
using System;

public class GenericAnimation : MonoBehaviour
{
    private Animator _animator;
    private BaseCollision _baseCollision;
    private Jump _jump;

    private Vector3 previousLocalPosition;
    private Vector3 simulatedVelocity;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _baseCollision = GetComponent<BaseCollision>();
        _jump = GetComponent<Jump>();
        previousLocalPosition = transform.localPosition;
    }

    void Update()
    {
        GetSimulatedVelocity();
        _animator.SetFloat("Horizontal Velocity", simulatedVelocity.x);
        _animator.SetFloat("Vertical Velocity", simulatedVelocity.y);

        if (_jump)
            _animator.SetFloat("Jump Velocity", _jump.jumpVelocity);

        if (Input.GetButtonDown("Fire1"))
            _animator.SetTrigger("Punch");
    }

    private void GetSimulatedVelocity()
    {
        if (previousLocalPosition != transform.localPosition)
        {
            simulatedVelocity.x = Mathf.Sign(transform.localPosition.x - previousLocalPosition.x);
            simulatedVelocity.y = Mathf.Sign(transform.localPosition.y - previousLocalPosition.y);
            previousLocalPosition = transform.localPosition;
        }
        else
            simulatedVelocity = Vector3.zero;
    }
}
