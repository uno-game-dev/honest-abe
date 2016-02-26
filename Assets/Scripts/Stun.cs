using UnityEngine;
using System.Collections;
using System;

public class Stun : MonoBehaviour
{
    public enum State { Null, Stunned }

    public State state;
    public float stunDuration = 0.92f;

    private float stunTimer = 0;
    private CharacterState _characterState;
    private BaseCollision _collision;
    private Animator _animator;

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        _characterState = GetComponent<CharacterState>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _collision.OnCollision += OnCollision;
    }

    private void OnDisable()
    {
        state = State.Null;
        _collision.OnCollision -= OnCollision;
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Damage")
            GetStunned();
    }

    private void Update()
    {
        if (state != State.Stunned)
            return;

        stunTimer += Time.deltaTime;
        if (stunTimer >= stunDuration)
            FinishStun();
    }

    public void GetStunned()
    {
        _animator.SetTrigger("Stun");
        state = State.Stunned;
        _characterState.SetState(CharacterState.State.Stun);
        stunTimer = 0;
    }

    private void FinishStun()
    {
        state = State.Null;
        _characterState.SetState(CharacterState.State.Idle);
    }
}
