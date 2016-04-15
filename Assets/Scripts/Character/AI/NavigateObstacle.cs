using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class NavigateObstacle : ConditionNode {

	private GameObject player;
	private EnemyFollow enemyFollow;
	private Vector2 selfPosition, playerPosition, direction, newDirection, deltaPosition;
	private float xDiff, yDiff,	distanceToPlayer, diagonalMovementSpeed;
	private LayerMask layerMask;
	private RaycastHit2D hit, topHit, bottomHit, leftHit, rightHit;
	private BaseCollision baseCollision;
	private Movement movement;

	override public void Start(){
		player = GameObject.Find ("Player");
		enemyFollow = self.GetComponent<EnemyFollow> ();
		layerMask = LayerMask.GetMask("Environment");
		baseCollision = self.GetComponent<BaseCollision> ();
		movement = self.GetComponent<Movement> ();
		diagonalMovementSpeed = (movement.horizontalMovementSpeed + movement.vericalMovementSpeed) / 2;

		// Stop following
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
	}

	public override Status Update () {	

		// Calculate direction and distance to player
		selfPosition = self.transform.position;
		playerPosition = player.transform.position;
		direction = (playerPosition - selfPosition).normalized;
		distanceToPlayer = (playerPosition - selfPosition).magnitude;

		// Fire 2 raycasts at the player and see which ones hit the obstacle
		xDiff = Mathf.Abs (playerPosition.x - selfPosition.x);
		yDiff = Mathf.Abs (playerPosition.y - selfPosition.y);
		if (xDiff > yDiff) {
			topHit = Physics2D.Raycast (selfPosition + new Vector2 (0, 0.5f), direction, distanceToPlayer, layerMask);
			bottomHit = Physics2D.Raycast (selfPosition + new Vector2 (0, -0.5f), direction, distanceToPlayer, layerMask);

			// If only the bottom one hits, veer up
			if (bottomHit && bottomHit.collider.tag == "Obstacle") {
				if (!(topHit && topHit.collider.tag == "Obstacle")) {
					veerUp ();
					return Status.Running;
				} else navigate ('y'); // If they both hit, navigate around the obstacle
			} else {
				// If only the top one hits, veer down
				if (topHit && topHit.collider.tag == "Obstacle") {
					veerDown ();
					return Status.Running;
				}
			}
		} else {
			rightHit = Physics2D.Raycast (selfPosition + new Vector2 (0.5f, 0), direction, distanceToPlayer, layerMask);
			leftHit = Physics2D.Raycast (selfPosition + new Vector2 (-0.5f, 0), direction, distanceToPlayer, layerMask);

			// If only the left one hits, veer right
			if (leftHit && leftHit.collider.tag == "Obstacle") {
				if (!(rightHit && rightHit.collider.tag == "Obstacle")) {
					veerRight ();
					return Status.Running;
				} else navigate('x'); // If they both hit, navigate around the obstacle
			} else {
				// If only the right one hits, veer left
				if (rightHit && rightHit.collider.tag == "Obstacle" ) {
					veerLeft ();
					return Status.Running;
				}
			}
		}
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}

	/**
	 * veerDirection():
	 *   Makes characters avoid obstacles from a distance by veering off in the specified direction on one axis,
	 *   using the player's position to determine which direction to move on the other axis.
	 *   veerUp() and veerDown() check to avoid ceiling and floor.
	 */
	public void veerRight(){
		if (playerPosition.y > selfPosition.y)
			deltaPosition = new Vector3 (1, 1, 0);
		else
			deltaPosition = new Vector3 (1, -1, 0);
		baseCollision.Move (Time.deltaTime * deltaPosition.normalized * diagonalMovementSpeed);
	}

	public void veerLeft(){
		if (playerPosition.y > selfPosition.y)
			deltaPosition = new Vector3 (-1, 1, 0);
		else
			deltaPosition = new Vector3 (-1, -1, 0);
		baseCollision.Move (Time.deltaTime * deltaPosition.normalized * diagonalMovementSpeed);
	}

	public void veerUp(){
		if (playerPosition.x > selfPosition.x)
			deltaPosition = new Vector3 (1, 1, 0);
		else
			deltaPosition = new Vector3 (-1, 1, 0);
		// I can't veer up if I'm right below the skyline
		if (selfPosition.y + deltaPosition.normalized.y * diagonalMovementSpeed < -0.1f)
			baseCollision.Move (Time.deltaTime * deltaPosition.normalized * diagonalMovementSpeed);
		else
			navigate('y'); // If I can't veer up, navigate using the y axis
	}

	public void veerDown(){
		if (playerPosition.x > selfPosition.x)
			deltaPosition = new Vector3 (1, -1, 0);
		else
			deltaPosition = new Vector3 (-1, -1, 0);
		// I can't veer down if I'm right above the bottom
		if (selfPosition.y - deltaPosition.normalized.y * diagonalMovementSpeed > -11.2f)
			baseCollision.Move (Time.deltaTime * deltaPosition.normalized * diagonalMovementSpeed);
		else
			navigate ('y'); // If I can't veer down, navigate using the y axis
	}

	/**
	 * Finds a way around the obstacle using the specified axis, when veering off won't work
	 */ 
	public void navigate(char axis){
		int i = 1;
		if (axis == 'y') {
			while (i < 20) { // gradually increase angle
				if (checkUp (i)) { // if I can move up at this angle
					newDirection = (direction + new Vector2 (0, i + 1)).normalized;
					baseCollision.Move (Time.deltaTime * newDirection * diagonalMovementSpeed);
					return;
				}
				if (checkDown (i)) { // if I can move down at this angle
					newDirection = (direction + new Vector2 (0, -i - 1)).normalized;
					baseCollision.Move (Time.deltaTime * newDirection * diagonalMovementSpeed);
					return;
				}
				i++;
			} // If I get here, I'm stuck.
			// TODO: Figure out how to get un-stuck. Only in NavigateObstacle.
			Debug.Log("stuck");
		} else {
			while (i < 20) {
				if (checkRight (i)) { // if I can move right at this angle
					newDirection = (direction + new Vector2 (i+1, 0)).normalized;
					baseCollision.Move(Time.deltaTime * newDirection * diagonalMovementSpeed);
					return;
				}
				if (checkLeft (i)) { // if I can move left at this angle
					newDirection = (direction + new Vector2 (-i-1, 0)).normalized;
					baseCollision.Move (Time.deltaTime * newDirection * diagonalMovementSpeed);
					return;
				}
				i++;
			} // If I get here, I'm stuck.
			Debug.Log("stuck");
		}
	}

	/** 
	 * checkDirection():
	 *   Checks if the character can move in the specified direction
	 */ 
	public bool checkUp(int i){
		if (selfPosition.y + i + 1 >= -0.1f) // can't go above the ground
			return false;
		// TODO: Mess with length = 2 and see if there's a better way?
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i), 2, layerMask); //length 2
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, 3), direction + new Vector2 (0, i), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}

	public bool checkDown(int i){
		if (selfPosition.y - i - 1 <= -11.2f) // can't go below the bottom
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, -3), direction + new Vector2(0, -i), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}

	public bool checkRight(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}

	public bool checkLeft(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}
}
