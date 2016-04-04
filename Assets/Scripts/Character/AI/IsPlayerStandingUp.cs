using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerStandingUp : ConditionNode {

	private GameObject player;
	private KnockDown knockDown;

	public override void Start () {
		player = GameObject.Find ("Player");
		knockDown = player.GetComponent<KnockDown> ();
	}
	
	public override Status Update () {
		if (knockDown.state == KnockDown.State.GettingUp || knockDown.state == KnockDown.State.Null) {
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		}
		return Status.Failure;
	}
}
