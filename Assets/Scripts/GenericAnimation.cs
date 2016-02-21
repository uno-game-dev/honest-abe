using UnityEngine;
using System.Collections;
using System;

public class GenericAnimation : MonoBehaviour
{
    private Animator _animator;
    private BaseCollision _baseCollision;
    private Movement _jump;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _baseCollision = GetComponent<BaseCollision>();
        _jump = GetComponent<Movement>();
    }
    
    void Update()
    {
        _animator.SetFloat("Horizontal Velocity", _baseCollision.Velocity.x / Time.deltaTime);
        _animator.SetFloat("Vertical Velocity", _baseCollision.Velocity.y / Time.deltaTime);

        if (_jump)
            _animator.SetFloat("Jump Velocity", _jump.jumpVelocity);
    }
}
