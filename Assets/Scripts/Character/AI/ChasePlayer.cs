using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ChasePlayer : ActionNode {

	// Called once when the node is created
	public override void Awake () {
	}

	// Called when the owner (BehaviourTree or ActionState) is enabled
	public override void OnEnable () {
	}

	// Called when the owner (BehaviourTree or ActionState) is disabled
	public override void OnDisable () {
	}

	// Called when the node starts its execution
	public override void Start () {}

	// This function is called when the node is in execution
	public override Status Update () {

		//Vector3 cameraPosition = GameObject.Find ("Main Camera").transform.position;
		//Vector3 topRight = cameraPosition + new Vector3 (20, 10, 0);
		//Vector3 bottomRight = cameraPosition + new Vector3 (20, -9, 0);
		//Vector3 topLeft = cameraPosition + new Vector3 (-20, 10, 0);
		//Vector3 bottomLeft = cameraPosition + new Vector3 (-20, -9, 0);

		self.GetComponent<EnemyFollow> ().targetType = EnemyFollow.TargetType.Player;
		spin (0.5f);

		// Never forget to set the node status
		return Status.Running;
	}

	public IEnumerator spin(float n){
		yield return new WaitForSeconds (n);
	}

	// Called when the node ends its execution
	public override void End () {}

	// This function is called to reset the default values of the node
	public override void Reset () {}

	// Called when the script is loaded or a value is changed in the inspector (Called in the editor only)
	public override void OnValidate () {}

}
