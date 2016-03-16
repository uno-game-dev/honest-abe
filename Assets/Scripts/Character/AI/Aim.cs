using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class Aim : ConditionNode {

	// Use this for initialization
	override public void Start () {

	}

	// Update is called once per frame
	override public Status Update () {
		if (onSuccess.id != 0)
			owner.root.SendEvent(onSuccess.id);
		return Status.Success;
	}
}
