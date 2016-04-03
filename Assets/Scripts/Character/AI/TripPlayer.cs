using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class TripPlayer : ActionNode {

	private Attack attack;

	public override void Start () {
		attack = self.GetComponent<Attack> ();
	}
	
	public override Status Update () {
		attack.HeavyAttack ();
		return Status.Success;
	}
}
