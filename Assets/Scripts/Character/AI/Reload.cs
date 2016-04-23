using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class Reload : ConditionNode {

	private int i;
	private int numFrames = 200;

	// Use this for initialization
	override public void Start () {
		i = 0;
	}

	// Update is called once per frame
	override public Status Update () {
		i++;
		if (i > numFrames) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		return Status.Running;
	}
}
