using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AttackPlayer : ActionNode {

	public override void Start () {}

	public override Status Update () {
		if (Random.value > 0.35) {
			self.GetComponent<Attack> ().LightAttack ();
			spin (0.5f);
		} else {
			self.GetComponent<Attack>().HeavyAttack();
			spin (2f);
		}

		return Status.Success;
	}

	public IEnumerator spin(float n){
		yield return new WaitForSeconds (n);
	}

}
