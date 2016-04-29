using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AmIOnScreen : ConditionNode {

	private Camera camera;
	private Vector3 bottomRight, bottomLeft, selfPosition;

	// Use this for initialization
	public override void Start () {
		camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}

	// Update is called once per frame
	public override Status Update () {

		selfPosition = self.transform.position;
		bottomRight = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
		bottomLeft = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));

		if (selfPosition.x > bottomLeft.x && selfPosition.x < bottomRight.x) {
			if(onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}

		return Status.Failure;
	}
}
