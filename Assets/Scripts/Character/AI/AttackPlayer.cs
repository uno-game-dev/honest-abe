using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AttackPlayer : ActionNode {

	public override void Start () {
	
	}

	public override Status Update () {
		if (Random.value > 0.35) {
			self.GetComponent<Attack> ().LightAttack ();
		} else {
			self.GetComponent<Attack> ().HeavyAttack ();
		}
		return Status.Success;
	}
}
