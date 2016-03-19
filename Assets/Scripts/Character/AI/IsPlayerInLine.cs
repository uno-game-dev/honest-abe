using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerInLine : ConditionNode {

	private GameObject player;

	// Use this for initialization
	override public void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	override public Status Update () {
		if (Mathf.Abs (player.transform.position.y - self.transform.position.y) < 0.5) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		return Status.Failure;
	}
}
