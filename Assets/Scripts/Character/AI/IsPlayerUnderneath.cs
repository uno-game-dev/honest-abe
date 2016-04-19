using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerUnderneath : ConditionNode {

	private GameObject player;
	private Vector2 playerPosition;
	private Vector2 selfPosition;

	// Use this for initialization
	public override void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	public override Status Update () {
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;
		float xWidth = 3;
		float yWidth = 2;
		if (playerPosition.x < selfPosition.x + xWidth && playerPosition.x > selfPosition.x - xWidth) {
			if (playerPosition.y < selfPosition.y + yWidth && playerPosition.y > selfPosition.y - yWidth) {
				if (onSuccess.id != 0)
					owner.root.SendEvent (onSuccess.id);
				Debug.Log ("Underneath");
				return Status.Success;
			}
		}
		return Status.Failure;
	}
}
