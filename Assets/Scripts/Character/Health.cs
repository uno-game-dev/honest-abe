using UnityEngine;
using BehaviourMachine;
using System;

public class Health : MonoBehaviour
{
	public int health;
	public int additionalHealthFloor;
	public int additionalHealthCeiling;
	public bool alive;

	private Boss _boss;
	private System.Random _rnd;
	private Attack playerAttack;
	private GameObject player;
	private Blackboard blackboard;
	private EnemyFollow enemyFollow;

	void Awake()
	{
		_rnd = new System.Random();
		health += _rnd.Next(additionalHealthFloor, additionalHealthCeiling + 1);
		alive = true;
		player = GameObject.Find ("Player");
		playerAttack = player.GetComponent<Attack>();
		blackboard = GetComponent<Blackboard> ();
		enemyFollow = GetComponent<EnemyFollow> ();
	}

	public void RandomizeHealth()
	{
	}

	public virtual void Increase(int amount)
	{
		health += amount;
	}

	public virtual void Decrease(int damage)
	{
		health -= damage;
		//If the hit would kill the gameObject
		if (health <= 0)
		{
			health = 0;
			alive = false;
			// Execution Check
			if (gameObject.tag == "Enemy")
			{
				if (gameObject.tag != "Player" && playerAttack.attackState == Attack.State.Heavy)
				{
					ShowExecution();
					EventHandler.SendEvent(EventHandler.Events.HEAVY_KILL);
				}
				else if (playerAttack.attackState == Attack.State.Light)
					EventHandler.SendEvent(EventHandler.Events.LIGHT_KILL);
			}

			// AI stuff: Mark this enemy's position around the player as available
			float attackPosition = blackboard.GetFloatVar("attackPosition");
			if (attackPosition != -1) {
				string side = blackboard.GetStringVar ("attackSide");
				GlobalBlackboard.Instance.GetBoolVar (side + "pos" + attackPosition).Value = false;
			}
			// Destroy the target gameobject this enemy was following
			if (enemyFollow.target != null)
				Destroy (enemyFollow.target);

			DeathSequence();
		}
	}

	public void ShowExecution()
	{
		GameObject number = new GameObject();
		number.name = "Execution";
		TextMesh tm = number.AddComponent<TextMesh>();
		tm.text = "RIP";
		tm.fontSize = 24;
		tm.color = Color.red;
		tm.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		tm.transform.position = transform.position;
		FloatUpAndDestroy f = number.AddComponent<FloatUpAndDestroy>();
		f.floatGravityMultiplier = 0.5f;
		f.floatVelocity = 2;
	}

	protected void DeathSequence()
	{
		Death death = GetComponent<Death>();
		if (death)
			death.enabled = true;
		else
			Destroy(gameObject);
	}
}
