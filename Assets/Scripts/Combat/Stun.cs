﻿using UnityEngine;
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
	private float knockbackAmount;
	private Vector2 previousPos, currentPos;
	private GenericAnimation genericAnimation;
	private System.Random random;

	private void Awake()
	{
		_collision = GetComponent<BaseCollision>();
		_characterState = GetComponent<CharacterState>();
		_animator = GetComponent<Animator>();
		genericAnimation = GetComponent<GenericAnimation>();
		random = new System.Random ();
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
			if (tag != "Player" || random.Next() > 0.5) { // only 50% chance to stun if it's Abe				
				Attack attack = collider.GetComponentInParent<Attack> ();
				float directionMod = (collider.GetComponentInParent<Movement> ().direction == Movement.Direction.Right ? 1f : -1f);
				GetStunned (attack.GetStunAmount (), attack.GetKnockbackAmount (), directionMod);
			}
		}
	}

	private void Update()
	{
		if (state != State.Stunned)
		{
			previousPos = transform.position;
			return;
		}

		currentPos = transform.position;
		if (Math.Abs(previousPos.x - currentPos.x) >= knockbackAmount / 4)
		{
			velocity = Vector2.zero;
		}

		stunDuration -= Time.deltaTime;
		_collision.Move(velocity * Time.deltaTime);
		if (stunDuration <= 0f)
			FinishStun();
	}

	public void GetStunned( float stunAmount = 1f, float knockbackAmount = 0.1f, float directionModifier = 1f )
	{
		if (_characterState.state == CharacterState.State.Grabbed)
		{
			_animator.Play("Grabbed Damage",0,0.15f);
			Invoke("FinishGrabbedStun", 0.5f);
			return;
		}
		this.knockbackAmount = knockbackAmount;
		_animator.Play("Light Damage Reaction", 0, 0.25f);
		state = State.Stunned;
		velocity = new Vector2((directionModifier), 0).normalized * knockbackAmount * 2;
		_characterState.SetState(CharacterState.State.Stun);
		if (tag == "Player")
			stunDuration = stunAmount;
		else
			stunDuration = stunAmount * 2;
	}

	private void FinishStun()
	{
		state = State.Null;
		velocity = Vector2.zero;
		_characterState.SetState(CharacterState.State.Idle);
	}

	private void FinishGrabbedStun()
	{
		if (genericAnimation) genericAnimation.UpdateState();
		_animator.Play("Grabbed");
	}
}
