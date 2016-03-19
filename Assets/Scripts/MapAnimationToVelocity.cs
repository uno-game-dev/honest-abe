using UnityEngine;
using System.Collections;

public class MapAnimationToVelocity : MonoBehaviour
{
    public Vector2 velocity;
    private Animator _animator;

    private int _horizontalVelocityId;
    private int _verticalVelocityId;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _horizontalVelocityId = _animator.GetInteger("Horizontal Velocity");
        _verticalVelocityId = _animator.GetInteger("Vertical Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

        _animator.SetFloat("Horizontal Velocity", velocity.x);
        _animator.SetFloat("Vertical Velocity", velocity.y);

        if (Input.GetButtonDown("Fire1"))
            _animator.SetTrigger("Punch");
    }
}
