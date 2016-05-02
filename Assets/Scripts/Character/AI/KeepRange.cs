using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class KeepRange : ActionNode {

	private GameObject player;
	private float distanceToPlayer;
	private Vector2 playerPosition, selfPosition;
	private float directionX;
	private float directionY;
    private Movement movement;
	private EnemyFollow enemyFollow;
	private Camera camera;
	private Vector3 cameraBottomRight;

    // Use this for initialization
    override public void Start () {
		player = GameObject.Find ("Player");
        movement = self.GetComponent<Movement>();
		enemyFollow = self.GetComponent<EnemyFollow> ();
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
		camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	override public Status Update () {

		// Get x- and y-directions to the player
		playerPosition = player.transform.position;
		selfPosition = self.transform.position;

		if (playerPosition.x < selfPosition.x)
			directionX = -1;
		else
			directionX = 1;

		if (playerPosition.y < selfPosition.y)
			directionY = -1;
		else
			directionY = 1;

		// Get camera position
		cameraBottomRight = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));

		// If he's too close, move away from him on x axis (but not off screen)
		distanceToPlayer = Mathf.Abs (playerPosition.x - selfPosition.x);
		if (distanceToPlayer <= blackboard.GetFloatVar ("preferredRangeMin")) {
			if (Time.deltaTime * (selfPosition.x - directionX) * movement.horizontalMovementSpeed > cameraBottomRight.x) {
				// Always move toward him on y axis
				movement.Move (new Vector2 (-directionX, directionY) * movement.horizontalMovementSpeed);
				// Face the player when moving away
				movement.FlipDirection ();
			} else {
				movement.Move (new Vector2 (0, directionY) * movement.horizontalMovementSpeed);
			}
		}
		// If he's too far away, move toward him on x axis
		else if (distanceToPlayer >= blackboard.GetFloatVar ("preferredRangeMax"))
			movement.Move (new Vector2 (directionX, directionY) * movement.horizontalMovementSpeed);
		else
			movement.Move (new Vector2 (0, directionY) * movement.vericalMovementSpeed);

		return Status.Success;
	}
}
