using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPathObstructed : ConditionNode {

	GameObject player;

	// Use this for initialization
	override public void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	override public Status Update () {

		// Fire three raycasts at the player
		Vector2 selfPosition = self.transform.position;
		Vector2 playerPosition = player.transform.position;
		Vector2 direction = playerPosition - selfPosition;
		float xDiff = Mathf.Abs (playerPosition.x - selfPosition.x);
		float yDiff = Mathf.Abs (playerPosition.y - selfPosition.y);
		float distanceToPlayer = Mathf.Sqrt (Mathf.Pow(xDiff,2) + Mathf.Pow(yDiff,2));
		LayerMask layerMask = LayerMask.GetMask("Environment");

		// Fire the middle ray first
		RaycastHit2D hit = Physics2D.Raycast (selfPosition, direction, distanceToPlayer, layerMask);
		bool obstructed = false;
		if (hit && hit.collider.tag == "Obstacle")
			obstructed = true;
		Debug.DrawRay (selfPosition, direction);

		if (xDiff > yDiff) {
			// Fire a ray above
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction, distanceToPlayer, layerMask);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
			Debug.DrawRay (selfPosition + new Vector2 (0, 1), direction);
			// Fire a ray below
			hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction, distanceToPlayer, layerMask);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
			Debug.DrawRay (selfPosition + new Vector2 (0, -1), direction);
		} else if (yDiff > xDiff) {
			// Fire a ray to the right
			hit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction, distanceToPlayer, layerMask);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
			Debug.DrawRay (selfPosition + new Vector2 (1, 0), direction);
			// Fire a ray to the left
			hit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction, distanceToPlayer, layerMask);
			if (hit && hit.collider.tag == "Obstacle")
				obstructed = true;
			Debug.DrawRay (selfPosition + new Vector2 (-1, 0), direction);
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
