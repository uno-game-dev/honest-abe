using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class KeepRange : ActionNode {

	private GameObject player;
	private float distanceToPlayer;
	private Vector2 playerPosition;
	private Vector2 selfPosition;
	private BaseCollision baseCollision;

	// Use this for initialization
	override public void Start () {
		player = GameObject.Find ("Player");
		baseCollision = self.GetComponent<BaseCollision> ();
	}
	
	// Update is called once per frame
	override public Status Update () {
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;
		distanceToPlayer = Mathf.Abs (playerPosition.x - selfPosition.x);
		if (distanceToPlayer <= blackboard.GetFloatVar ("preferredRangeMin")) {
			if (playerPosition.x > selfPosition.x) {
				//self.transform.position = (Vector2)self.transform.position + new Vector2 (-0.07f, 0);
				baseCollision.Move (new Vector3 (-0.07f, 0, 0));
			} else {
				//self.transform.position = (Vector2)self.transform.position + new Vector2 (0.07f, 0);
				baseCollision.Move (new Vector3 (0.07f, 0, 0));
			}
		}
		if (playerPosition.y > selfPosition.y) {
			//self.transform.position = (Vector2)self.transform.position + new Vector2 (0, 0.07f);
			baseCollision.Move (new Vector3 (0, 0.07f, 0));
		} else if (playerPosition.y < selfPosition.y) {
			//self.transform.position = (Vector2)self.transform.position + new Vector2 (0, -0.07f);
			baseCollision.Move(new Vector3(0, -0.07f, 0));
		}

		return Status.Success;
	}
}
