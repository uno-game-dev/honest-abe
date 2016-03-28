using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class FindAttackPosition : ActionNode {

	private GameObject player;
	private EnemyFollow enemyFollow;
	private Vector3[] positions;
	private GameObject target;

	public override void Start () {

		player = GameObject.Find ("Player");
		enemyFollow = self.GetComponent<EnemyFollow> ();

		// Find my target or create one
		if (enemyFollow.target == null)
			target = new GameObject ();
		else
			target = enemyFollow.target;

		// Define attack positions (offsets from the player's position)
		positions = new Vector3[12] {
			new Vector3(1, 0, 0), // right
			new Vector3(-1, 0, 0), // left
			new Vector3(1, -1f, 0), // right/down
			new Vector3(1, 1f, 0), // right/up
			new Vector3(-1, -1f, 0), // left/down
			new Vector3(-1, 1f, 0), // left/up
			new Vector3(2f, 0, 0), // right/right
			new Vector3(-2f, 0, 0), // left/left
			new Vector3(2f, -1f, 0), // right/right/down
			new Vector3(2f, 1f, 0), // right/right/up
			new Vector3(-2f, -1f, 0), // left/left/down
			new Vector3(-2f, 1f, 0) // left/left/up
		};
	}
	
	public override Status Update () {

		// Unclaim my attack position, since I'm not there yet
		float attackPosition = blackboard.GetFloatVar("attackPosition");
		if (attackPosition != -1) {
			GlobalBlackboard.Instance.GetBoolVar ("pos" + attackPosition).Value = false;
			blackboard.GetFloatVar ("attackPosition").Value = -1;
		}

		int i = 0;
		// Find the best available position around Abe
		for (i = 0; i < 13; i++) {
			if (i != 12) {
				if (!GlobalBlackboard.Instance.GetBoolVar ("pos" + i)) {
					// Claim it
					GlobalBlackboard.Instance.GetBoolVar ("pos" + i).Value = true;
					blackboard.GetFloatVar ("attackPosition").Value = i;
					// Put my target at that position
					target.transform.position = player.transform.position + positions[i];
					enemyFollow.targetType = EnemyFollow.TargetType.TargetGameObject;
					enemyFollow.target = target;
					// Change my stop distance to 0 so that I go all the way to that spot
					enemyFollow.stopDistanceX = 0;
					enemyFollow.stopDistanceY = 0;
					break;
				}
			} else {
				// If all the positions are full, put my target at the 0 position anyway, but don't claim it
				target.transform.position = player.transform.position + positions[0];
				enemyFollow.targetType = EnemyFollow.TargetType.TargetGameObject;
				enemyFollow.target = target;
				enemyFollow.stopDistanceX = 0;
				enemyFollow.stopDistanceY = 0;
			}
		}

		return Status.Running;
	}
}
