using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ChasePlayer : ActionNode {

	public override Status Update () {

		self.GetComponent<EnemyFollow> ().targetType = EnemyFollow.TargetType.Player;
		return Status.Running;
	}
}
