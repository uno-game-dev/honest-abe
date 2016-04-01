using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPositionReached : ConditionNode {

	private GameObject target;
	private GameObject player;
	private EnemyFollow enemyFollow;
	private Vector2 targetPosition;
	private Vector2 selfPosition;
	private Vector2 playerPosition;

	public override void Start () {
		enemyFollow = self.GetComponent<EnemyFollow> ();
		player = GameObject.Find ("Player");
	}
	
	public override Status Update () {

		// Get my new target object
		target = enemyFollow.target;
		if (target == null)
			Debug.Log ("Error, called IsPositionReached without a target position");

		// Get positions of self and target
		selfPosition = self.transform.position;
		targetPosition = target.transform.position;
		playerPosition = player.transform.position;

		if (Mathf.Abs(targetPosition.x - selfPosition.x) < 1f && Mathf.Abs(targetPosition.y - selfPosition.y) < 1f) {
			// If target is to the right of the player, I need to be on the right side of the target
			if (targetPosition.x > playerPosition.x) {
				if (selfPosition.x >= targetPosition.x) {
					if (onSuccess.id != 0)
						owner.root.SendEvent (onSuccess.id);
					return Status.Success;
				}
			} else {
			// Otherwise I need to be on the left side of the target
				if (selfPosition.x <= targetPosition.x) {
					if (onSuccess.id != 0)
						owner.root.SendEvent (onSuccess.id);
					return Status.Success;
				}
			}
		}
		return Status.Failure;
	}
}
