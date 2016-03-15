using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerInSight : ConditionNode {

	// Update is called once per frame
	public override Status Update () {
		self.GetComponent<EnemyFollow> ().targetType = EnemyFollow.TargetType.Null;
		if (Mathf.Abs (GameObject.Find("Player").transform.position.x - self.transform.position.x) < blackboard.GetFloatVar("movementProximityDistance")) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		else
			return Status.Failure;
	}
}
