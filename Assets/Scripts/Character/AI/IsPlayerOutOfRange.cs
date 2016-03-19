using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class IsPlayerOutOfRange : ConditionNode {

	// Update is called once per frame
	public override Status Update () {
		if(Mathf.Abs(GameObject.Find("Player").transform.position.x - self.transform.position.x) < blackboard.GetFloatVar("attackProximityDistanceX")){
			if (Mathf.Abs (GameObject.Find("Player").transform.position.y - self.transform.position.y) < blackboard.GetFloatVar("attackProximityDistanceY")) {						
				return Status.Failure;
			}
		}
		if (onSuccess.id != 0)
			owner.root.SendEvent(onSuccess.id);		
		return Status.Success;
	}
}
