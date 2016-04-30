using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AttackPlayer : ConditionNode {

	private Attack attack;
	private Movement movement;
	private GameObject player;

	public override void Start () {
		attack = self.GetComponent<Attack> ();
		movement = self.GetComponent<Movement> ();
		player = GameObject.Find ("Player");
	}

	public override Status Update () {

		// Always face the right direction before you attack
		if (player.transform.position.x < self.transform.position.x)
			movement.SetDirection (Movement.Direction.Left);
		else
			movement.SetDirection (Movement.Direction.Right);

		if (self.name.Contains ("Bushwhacker")) {
			attack.LightAttack ();
		} else {
			// Attack Abe
			if (Random.value > 0.35) {
				attack.LightAttack ();
			} else {
				attack.HeavyAttack ();
			}
		}
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}
}
