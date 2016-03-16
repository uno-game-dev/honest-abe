using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ChasePlayer : ActionNode {

	public override Status Update () {

		self.GetComponent<EnemyFollow> ().targetType = EnemyFollow.TargetType.Player;
		//spin (0.5f);

		return Status.Running;
	}

	public IEnumerator spin(float n){
		yield return new WaitForSeconds (n);
	}
}
