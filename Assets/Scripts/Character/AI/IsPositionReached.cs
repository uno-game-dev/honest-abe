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
	private float xDistance;
	private float yDistance;
	private Grabber grabber;

	public override void Start () {
		enemyFollow = self.GetComponent<EnemyFollow> ();
		player = GameObject.Find ("Player");
		xDistance = 1f;
		yDistance = 1f;
		grabber = player.GetComponent<Grabber> ();
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

		// If the player is grabbing somebody, increase the distance threshold
		if (grabber.state == Grabber.State.Hold)
			xDistance = 1.8f;

		if (Mathf.Abs(targetPosition.x - selfPosition.x) < xDistance && Mathf.Abs(targetPosition.y - selfPosition.y) < yDistance) {
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
