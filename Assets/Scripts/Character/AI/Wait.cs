using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class Wait : ConditionNode {

	private float timer, duration;

	public override void Start(){
		timer = 0;
		duration = blackboard.GetFloatVar ("waitTime");
	}

	public override Status Update () {
		timer += Time.deltaTime;
		if (timer > duration) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		return Status.Running;
	}
}
