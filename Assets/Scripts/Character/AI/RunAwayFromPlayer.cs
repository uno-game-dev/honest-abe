using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class RunAwayFromPlayer : ConditionNode {

	private BaseCollision baseCollision;
	private GameObject player;
	private Vector3 playerPosition, selfPosition, cameraBottomRight;
	private Movement movement;
	private EnemyFollow enemyFollow;
	private Vector3 deltaPosition;
	private float directionX, directionY, distanceToPlayer;
	private float timer, duration;
	private Camera camera;

	public override void Start () {
		timer = 0;
		duration = 2.5f;
		player = GameObject.Find ("Player");
		baseCollision = self.GetComponent<BaseCollision> ();
		movement = self.GetComponent<Movement> ();
		enemyFollow = self.GetComponent<EnemyFollow> ();
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
		camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}

	public override Status Update () {
		
		// Get camera bottom right coordinates
		cameraBottomRight = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));

		// Move in the opposite direction of the player, but not off screen
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;

		Vector3 vectorToPlayer = playerPosition - selfPosition;
		deltaPosition = -vectorToPlayer.normalized * movement.horizontalMovementSpeed;
		float newY = selfPosition.y + deltaPosition.y;
		float newX = selfPosition.x + deltaPosition.x;
		if (newY < -0.1f && newY > -11.2f && newX < cameraBottomRight.x) {
			movement.SetState (Movement.State.Walk);
			baseCollision.Move (Time.deltaTime * deltaPosition);
		} else { // If I can't go any further, go ahead and turn around
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}

		// Do that for some amount of time, then go back to Approach
		timer += Time.deltaTime;
		if (timer > duration) {
			if (onSuccess.id != 0)
				owner.root.SendEvent(onSuccess.id);
			return Status.Success;
		}
		Debug.Log (timer);
		return Status.Running;
	}
}
