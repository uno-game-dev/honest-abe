using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class RunToDistance : ActionNode {

	private BaseCollision baseCollision;
	private GameObject player;
	private Vector3 playerPosition;
	private Vector3 selfPosition;

	public override void Start () {
		player = GameObject.Find ("Player");
		baseCollision = self.GetComponent<BaseCollision> ();
	}

	public override Status Update () {
		// Move in the opposite direction of the player
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;
		Vector3 vectorToPlayer = playerPosition - selfPosition;
		baseCollision.Move(Vector3.ClampMagnitude(vectorToPlayer * -1, 0.14f));
		return Status.Success;
	}
}
