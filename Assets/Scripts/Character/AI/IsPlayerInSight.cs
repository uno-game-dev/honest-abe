using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerInSight : ConditionNode {

	private GameObject player;
	private float movementProximityDistance;

	public override void Start(){
		player = GameObject.Find ("Player");
		movementProximityDistance = blackboard.GetFloatVar ("movementProximityDistance");
		self.GetComponent<EnemyFollow> ().targetType = EnemyFollow.TargetType.Null;
	}

	// Update is called once per frame
	public override Status Update () {
		if (Mathf.Abs (player.transform.position.x - self.transform.position.x) < movementProximityDistance) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		else
			return Status.Failure;
	}
}
