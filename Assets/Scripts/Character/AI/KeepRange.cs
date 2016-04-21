using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class KeepRange : ActionNode {

	private GameObject player;
	private float distanceToPlayer;
	private Vector2 playerPosition;
	private Vector2 selfPosition;
	private float directionX;
	private float directionY;
    private Movement movement;
	private EnemyFollow enemyFollow;

    // Use this for initialization
    override public void Start () {
		player = GameObject.Find ("Player");
        movement = self.GetComponent<Movement>();
		enemyFollow = self.GetComponent<EnemyFollow> ();
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
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

		// If he's too close, move away from him on x axis
		distanceToPlayer = Mathf.Abs (playerPosition.x - selfPosition.x);
		if (distanceToPlayer <= blackboard.GetFloatVar ("preferredRangeMin")) {
			// Always move toward him on y axis
			movement.Move (new Vector2 (-directionX, directionY) * movement.horizontalMovementSpeed);
			// Face the player when moving away
			movement.FlipDirection ();
		}
		// If he's too far away, move toward him on x axis
		else if (distanceToPlayer > blackboard.GetFloatVar ("preferredRangeMax"))
			movement.Move (new Vector2(directionX, directionY) * movement.horizontalMovementSpeed);

		return Status.Success;
	}
}
