using UnityEngine;
using System.Collections;
using System;

public class Stun : MonoBehaviour
{
    public enum State { Null, Stunned }

    public State state;
    public float stunDuration = 0.0f;

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

		if (collider.tag == "Damage"){
			Attack attack = collider.GetComponentInParent<Attack>();
			float directionMod = (collider.GetComponentInParent<Movement>().direction == Movement.Direction.Right ? 1f : -1f);
			GetStunned(attack.GetStunAmount(), attack.GetKnockbackAmount(), directionMod);
		}
    }

    private void Update()
    {
        if (state != State.Stunned)
            return;

        stunDuration -= Time.deltaTime;
        _collision.Move(velocity * Time.deltaTime);
        if (stunDuration <= 0f)
            FinishStun();
    }

	public void GetStunned( float stunAmount = 1f, float knockbackAmount = 0.1f, float directionModifier = 1f )
    {
        _animator.Play("Light Damage Reaction", 0, 0.25f);
        state = State.Stunned;
        velocity = new Vector2((directionModifier), 0).normalized * knockbackAmount;
        _characterState.SetState(CharacterState.State.Stun);
        stunDuration = stunAmount;
    }

    private void FinishStun()
    {
        state = State.Null;
        velocity = Vector2.zero;
        _characterState.SetState(CharacterState.State.Idle);
    }
}
