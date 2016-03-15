using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AttackPlayer : ActionNode {

	// Called once when the node is created
	public virtual void Awake () {}

	// Called when the owner (BehaviourTree or ActionState) is enabled
	public override void OnEnable () {}

	// Called when the node starts its execution
	public override void Start () {}

	// This function is called when the node is in execution
	public override Status Update () {
		if (Random.value > 0.35) {
			self.GetComponent<Attack> ().LightAttack ();
			spin (0.5f);
		} else {
			self.GetComponent<Attack>().HeavyAttack();
			spin (2f);
		}

		// Never forget to set the node status
		return Status.Success;
	}

	public IEnumerator spin(float n){
		yield return new WaitForSeconds (n);
	}

	// Called when the node ends its execution
	public override void End () {}

	// Called when the owner (BehaviourTree or ActionState) is disabled
	public override void OnDisable () {}

	// This function is called to reset the default values of the node
	public override void Reset () {}

	// Called when the script is loaded or a value is changed in the inspector (Called in the editor only)
	public override void OnValidate () {}
}
