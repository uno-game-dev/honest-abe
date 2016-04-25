﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LungeAttack : MonoBehaviour
{
	public float distance = 0f;
	public float traveled = 0f;
	public Vector2 velocity = Vector2.zero;

	private bool active = false;
	private BaseCollision _collision;
	private Vector3 prevPosition = Vector3.zero;
	public Movement.Direction _dir = Movement.Direction.Null;
	private bool lightAttack = true;
	private void Start()
	{
		_collision = this.GetComponentInParent<BaseCollision>();
	}
	private void Update()
	{
		if(active)
		{
			if(lightAttack)
				gameObject.GetComponent<Attack>().LightAttack();
			else
				gameObject.GetComponent<Attack>().HeavyAttack();

			Vector3 lungeStep = velocity;
			lungeStep.x *= _dir == Movement.Direction.Right ? 1 : -1;

			_collision.Move(lungeStep * Time.deltaTime);
			traveled += Vector3.Distance(_collision.transform.position, prevPosition);

			if( distance < Math.Abs(traveled) )
			{
				active = false;
				_collision.Move(Vector3.zero);
				traveled = 0f;
			

			}
		}
	}

	public void Lunge( BaseAttack.Strength strength = BaseAttack.Strength.Light)
	{
		//Move forward
		if ( !active)
		{
			active = true;
			_dir = gameObject.GetComponent<Movement>().direction;
			lightAttack = (strength == BaseAttack.Strength.Light ? true : false);
			prevPosition = _collision.transform.position;
			Debug.Log ("Lunging");
		}
	}
}