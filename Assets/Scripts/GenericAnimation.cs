using UnityEngine;
using System.Collections;
using System;

public class GenericAnimation : MonoBehaviour
{
    private Animator _animator;
    private BaseCollision _baseCollision;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _baseCollision = GetComponent<BaseCollision>();
    }
    
    void Update()
    {
        _animator.SetFloat("Horizontal Velocity", _baseCollision.Velocity.x / Time.deltaTime);
        _animator.SetFloat("Vertical Velocity", _baseCollision.Velocity.y / Time.deltaTime);
    }
}
