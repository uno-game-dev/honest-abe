using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AttackPlayer : ActionNode {

	private GameObject player;
	private Blackboard blackboard;

	public override void Start () {
		player = GameObject.Find ("Player");
		blackboard = self.GetComponent<Blackboard> ();
	}

	public override Status Update () {

		// Attack Abe
		if (Random.value > 0.35) {
			self.GetComponent<Attack> ().LightAttack ();
		} else {
			self.GetComponent<Attack> ().HeavyAttack ();
		}

		return Status.Success;
	}
}
