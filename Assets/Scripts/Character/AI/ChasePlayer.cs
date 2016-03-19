using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ChasePlayer : ActionNode {

	private EnemyFollow enemyFollow;

	public override void Start(){
		enemyFollow = self.GetComponent<EnemyFollow> ();
	}

	public override Status Update () {
		enemyFollow.targetType = EnemyFollow.TargetType.Player;
		return Status.Running;
	}
}
