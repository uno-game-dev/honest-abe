using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class RunAwayFromPlayer : ActionNode {

	private BaseCollision baseCollision;
	private GameObject player;
	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Movement movement;
	private EnemyFollow enemyFollow;

	public override void Start () {
		player = GameObject.Find ("Player");
		baseCollision = self.GetComponent<BaseCollision> ();
		movement = self.GetComponent<Movement> ();
		enemyFollow = self.GetComponent<EnemyFollow> ();
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
	}

	public override Status Update () {
		// Move in the opposite direction of the player
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;
		Vector3 vectorToPlayer = playerPosition - selfPosition;
		baseCollision.Move(Time.deltaTime * -vectorToPlayer.normalized * movement.horizontalMovementSpeed);
		return Status.Success;
	}
}
