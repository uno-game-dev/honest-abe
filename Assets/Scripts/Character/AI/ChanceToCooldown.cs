using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ChanceToCooldown : ConditionNode {

	private float cooldownChance;

	// Use this for initialization
	public override void Start () {
		cooldownChance = blackboard.GetFloatVar ("cooldownChance");
	}

	// Update is called once per frame
	public override Status Update () {
		if (Random.value < cooldownChance) {
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		}
		return Status.Failure;
	}
}
