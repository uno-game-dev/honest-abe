using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsAttackFinished : ConditionNode {

	private Attack attack;

	// Use this for initialization
	public override void Start () {
		attack = self.GetComponent<Attack> ();
	}

	// Update is called once per frame
	public override Status Update () {
		if (attack.attackState != Attack.State.Null)
			return Status.Failure;
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}
}
