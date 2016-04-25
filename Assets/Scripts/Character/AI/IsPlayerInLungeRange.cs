using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerInLungeRange : ConditionNode {

	private GameObject player;
	private float distanceToPlayer;

	// Use this for initialization
	override public void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	override public Status Update () {
		distanceToPlayer = Mathf.Abs(player.transform.position.x - self.transform.position.x);
		if (distanceToPlayer <= blackboard.GetFloatVar("lungeProximityDistance")) {
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		}
		return Status.Failure;
	}
}