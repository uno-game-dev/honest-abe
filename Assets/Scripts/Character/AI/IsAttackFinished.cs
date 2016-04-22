using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsAttackFinished : ConditionNode {

	private MeleeAttack meleeAttack;

	// Use this for initialization
	public override void Start () {
		meleeAttack = self.GetComponent<MeleeAttack> ();
	}

	// Update is called once per frame
	public override Status Update () {
		if (meleeAttack.state != BaseAttack.State.Null)
			return Status.Failure;
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}
}
