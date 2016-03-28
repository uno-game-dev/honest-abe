using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ChasePlayer : ActionNode {
	private EnemyFollow enemyFollow;
	private GameObject player;
	private BaseCollision baseCollision;

	public override void Start(){
		enemyFollow = self.GetComponent<EnemyFollow> ();
		player = GameObject.Find ("Player");
		baseCollision = self.GetComponent<BaseCollision> ();
	}

	public override Status Update () {

		// Follow the player
		enemyFollow.targetType = EnemyFollow.TargetType.Player;

		// Set my stop distance so that I don't run into the player
		enemyFollow.stopDistanceX = 2;
		enemyFollow.stopDistanceY = 0.1f;

		// Unclaim my attack position
		float attackPosition = blackboard.GetFloatVar ("attackPosition");
		if (attackPosition != -1) {
			GlobalBlackboard.Instance.GetBoolVar ("pos" + attackPosition).Value = false;
			blackboard.GetFloatVar ("attackPosition").Value = -1;
		}

		return Status.Running;
	}

}
