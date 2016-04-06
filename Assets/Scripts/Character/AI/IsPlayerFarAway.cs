using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerFarAway : ConditionNode {

	private GameObject player;
	private float distanceToPlayer;

	override public void Start () {
		player = GameObject.Find ("Player");
	}

	override public Status Update () {
		distanceToPlayer = Mathf.Abs(player.transform.position.x - self.transform.position.x);
		if (distanceToPlayer >= blackboard.GetFloatVar("farAwayDistance")) {
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		}
		return Status.Failure;
	}
}
