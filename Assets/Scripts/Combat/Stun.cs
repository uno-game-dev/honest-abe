using UnityEngine;
using System.Collections;
using System;

public class Stun : MonoBehaviour
{
    public enum State { Null, Stunned }

    public State state;
    public float stunDuration = 0.92f;
    public float knockbackSpeed = 10f;

    private Vector2 velocity = Vector2.zero;
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
        _collision.OnCollisionEnter += OnCollision;
    }

    private void OnDisable()
    {
        state = State.Null;
        _collision.OnCollisionEnter -= OnCollision;
    }

    private void OnCollision(Collider2D collider)
    {
        AttackArea attackArea = collider.GetComponent<AttackArea>();
        if (attackArea && attackArea.IsShootType())
            return;

        if (collider.tag == "Damage")
            GetStunned(collider);
    }

    private void Update()
    {
        if (state != State.Stunned)
            return;

        stunTimer += Time.deltaTime;
        _collision.Move(velocity * Time.deltaTime);
        if (stunTimer >= stunDuration)
            FinishStun();
    }

    public void GetStunned( Collider2D collider = null)
    {
        _animator.Play("Light Damage Reaction", 0, 0.25f);
        state = State.Stunned;
        velocity = new Vector2((_collision.transform.position.x - collider.transform.position.x ), 0).normalized * knockbackSpeed;
        _characterState.SetState(CharacterState.State.Stun);
        stunTimer = 0;
    }

    private void FinishStun()
    {
        state = State.Null;
        velocity = Vector2.zero;
        _characterState.SetState(CharacterState.State.Idle);
    }
}
