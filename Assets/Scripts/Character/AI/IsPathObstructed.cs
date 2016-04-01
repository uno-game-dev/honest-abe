using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPathObstructed : ConditionNode {

	GameObject player;
	Vector2 playerPosition;
	Vector2 selfPosition;
	Vector2 direction;
	float xDiff;
	float yDiff;
	float distanceToPlayer;
	LayerMask layerMask;
	RaycastHit2D hit;
	bool obstructed;

	override public void Start () {
		player = GameObject.Find ("Player");
		layerMask = LayerMask.GetMask ("Environment");
	}
	
	override public Status Update () {

		// Get new positions of self and player, plus direction and distance to player
		selfPosition = self.transform.position;
		playerPosition = player.transform.position;
		direction = playerPosition - selfPosition;
		xDiff = Mathf.Abs (playerPosition.x - selfPosition.x);
		yDiff = Mathf.Abs (playerPosition.y - selfPosition.y);
		distanceToPlayer = Mathf.Sqrt (Mathf.Pow(xDiff,2) + Mathf.Pow(yDiff,2));

		// Fire 2 raycasts at the player, offset from the center enough to represent a passable space
		obstructed = false;

		if (xDiff > yDiff) {
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 0.5f), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, 0.5f), direction);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -0.5f), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, -0.5f), direction);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
		} else if (yDiff > xDiff) {
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0.5f, 0), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0.5f, 0), direction);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
			hit = Physics2D.Raycast (selfPosition + new Vector2 (-0.5f, 0), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (-0.5f, 0), direction);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
		} else {
			Debug.Log ("Diagonal");
		}

		if (obstructed) {
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		}

		return Status.Failure;
	}
}
