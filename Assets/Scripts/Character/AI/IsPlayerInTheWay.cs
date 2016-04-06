using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerInTheWay : ConditionNode {

	GameObject target;
	EnemyFollow enemyFollow;
	Vector2 selfPosition;
	Vector2 targetPosition;
	Vector2 direction;
	float xDiff;
	float yDiff;
	float distanceToTarget;

	public override void Start () {
		enemyFollow = self.GetComponent<EnemyFollow> ();
	}
	
	public override Status Update () {

		// Get new target object
		target = enemyFollow.target;
		if (target == null)
			Debug.Log ("Error: Called IsPlayerInTheWay without a target object");

		// Get new positions of self and target, plus direction and distance to target
		selfPosition = self.transform.position;
		targetPosition = target.transform.position;
		direction = targetPosition - selfPosition;
		xDiff = Mathf.Abs (targetPosition.x - selfPosition.x);
		yDiff = Mathf.Abs (targetPosition.y - selfPosition.y);
		distanceToTarget = Mathf.Sqrt (Mathf.Pow(xDiff,2) + Mathf.Pow(yDiff,2));

		// Fire 2 raycasts at the target, offset from the center enough to represent a passable space
		LayerMask layerMask = LayerMask.GetMask("Player"); 
		RaycastHit2D hit; 
		bool obstructed = false;

		if (xDiff > yDiff) {
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0, .5f), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, .5f), direction);
			if (hit && hit.collider.tag == "Player")
				obstructed = true;
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -.5f), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, -.5f), direction);
			if (hit && hit.collider.tag == "Player")
				obstructed = true;
		} else if (yDiff > xDiff) {
			hit = Physics2D.Raycast (selfPosition + new Vector2 (.5f, 0), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (.5f, 0), direction);
			if (hit && hit.collider.tag == "Player")
				obstructed = true;
			hit = Physics2D.Raycast (selfPosition + new Vector2 (-.5f, 0), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (-.5f, 0), direction);
			if (hit && hit.collider.tag == "Player")
				obstructed = true;
		} else {
			Debug.Log ("Diagonal");
		}

		if (obstructed) {
			if (onSuccess.id != null)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		}
		return Status.Failure;
	}
}
