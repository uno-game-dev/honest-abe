using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ShootRifle : ConditionNode {

	private ShootAttack shootAttack;

	// Use this for initialization
	override public void Start () {
		shootAttack = self.GetComponent<ShootAttack> ();
		shootAttack.StartLightAttack ();
		return;
	}
	
	// Update is called once per frame
	override public Status Update () {
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}
}
