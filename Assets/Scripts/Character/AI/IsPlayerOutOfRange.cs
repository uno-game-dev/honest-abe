using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerOutOfRange : ConditionNode {

	private GameObject player;
	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Movement.Direction direction;

	public override void Start(){
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	public override Status Update () {
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;
		direction = self.GetComponent<Movement> ().direction;

		if(Mathf.Abs(playerPosition.x - selfPosition.x) < blackboard.GetFloatVar("attackProximityDistanceX")){
			if (Mathf.Abs (playerPosition.y - selfPosition.y) < blackboard.GetFloatVar("attackProximityDistanceY")) {						
				if (direction == Movement.Direction.Left && playerPosition.x < selfPosition.x) {
					return Status.Failure;
				}
				if (direction == Movement.Direction.Right && playerPosition.x > selfPosition.x) {
					return Status.Failure;
				}
			}
		}

		if (onSuccess.id != 0)
			owner.root.SendEvent(onSuccess.id);		
		return Status.Success;
	}
}
