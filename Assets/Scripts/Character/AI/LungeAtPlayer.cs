using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class LungeAtPlayer :ActionNode {

	private Attack attack;

	public override void Start () {
		attack = self.GetComponent<Attack> ();
	}

	public override Status Update () {
		// Attack Abe
			(self.GetComponent<LungeAttack> ()).Lunge(BaseAttack.Strength.Heavy);
		return Status.Success;
	}
}