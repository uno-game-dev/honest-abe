using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class Reload : ConditionNode {

    private float timer = 0;
    private float duration = 0;

	override public Status Update () {
        timer += Time.deltaTime;
		if (timer > duration) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		return Status.Running;
	}
}
