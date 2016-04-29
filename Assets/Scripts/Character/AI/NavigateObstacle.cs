using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class NavigateObstacle : ConditionNode {

	private GameObject player;
	private EnemyFollow enemyFollow;
	private Vector2 selfPosition, playerPosition, direction, directionNorm, newDirection, deltaPosition;
	private float xDiff, yDiff,	distanceToPlayer, diagonalMovementSpeed;
	private LayerMask layerMask;
	private RaycastHit2D hit, topHit, bottomHit, leftHit, rightHit;
	private BaseCollision baseCollision;
	private Movement movement;
	private bool bailing;

	override public void Start(){
		player = GameObject.Find ("Player");
		enemyFollow = self.GetComponent<EnemyFollow> ();
		layerMask = LayerMask.GetMask("Environment");
		baseCollision = self.GetComponent<BaseCollision> ();
		movement = self.GetComponent<Movement> ();
		diagonalMovementSpeed = (movement.horizontalMovementSpeed + movement.vericalMovementSpeed) / 2;
		bailing = false;

		// Stop following
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
	}

	public override Status Update () {	

		// Calculate direction and distance to player
		selfPosition = self.transform.position;
		playerPosition = player.transform.position;
		direction = playerPosition - selfPosition;
		directionNorm = direction.normalized;
		distanceToPlayer = (playerPosition - selfPosition).magnitude;

		// Patch annoying bug
		/*if (selfPosition.y <= -11.2f)
			baseCollision.Move (Time.deltaTime * new Vector2 (0, 1) * movement.vericalMovementSpeed);
		*/

		if (!bailing) {
			// Fire 2 raycasts at the player and see which ones hit the obstacle
			xDiff = Mathf.Abs (playerPosition.x - selfPosition.x);
			yDiff = Mathf.Abs (playerPosition.y - selfPosition.y);
			if (xDiff > yDiff) {
				topHit = Physics2D.Raycast (selfPosition + new Vector2 (0, 0.5f), directionNorm, distanceToPlayer, layerMask);
				bottomHit = Physics2D.Raycast (selfPosition + new Vector2 (0, -0.5f), directionNorm, distanceToPlayer, layerMask);


				// If only the bottom one hits, veer up
				if (bottomHit && bottomHit.collider.tag == "Obstacle") {
					if (!(topHit && topHit.collider.tag == "Obstacle")) {
						veerUp ();
					} else {
						// If they both hit, navigate around the obstacle.
						navigate ('y');
					}
				} else {
					// If only the top one hits, veer down
					if (topHit && topHit.collider.tag == "Obstacle") {
						veerDown ();
					}
				}
			} else {
				rightHit = Physics2D.Raycast (selfPosition + new Vector2 (0.5f, 0), directionNorm, distanceToPlayer, layerMask);
				leftHit = Physics2D.Raycast (selfPosition + new Vector2 (-0.5f, 0), directionNorm, distanceToPlayer, layerMask);

				// If only the left one hits, veer right
				if (leftHit && leftHit.collider.tag == "Obstacle") {
					if (!(rightHit && rightHit.collider.tag == "Obstacle")) {
						veerRight ();
					} else {
						// If they both hit, navigate around the obstacle
						navigate ('x');
					}
				} else {
					// If only the right one hits, veer left
					if (rightHit && rightHit.collider.tag == "Obstacle") {
						veerLeft ();
					}
				}
			}
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		} else {
			//bailOut ();
			return Status.Running;
		}
	}

	public void stepRight(){
		baseCollision.Move(Time.deltaTime * new Vector2(1, 0) * diagonalMovementSpeed);
	}

	public void stepLeft(){
		baseCollision.Move (Time.deltaTime * new Vector2 (-1, 0) * diagonalMovementSpeed);
	}

	public void stepUp(){
		if (Time.deltaTime * (selfPosition.y + 1) * diagonalMovementSpeed < -0.1f)
			baseCollision.Move (Time.deltaTime * new Vector2 (0, 1) * diagonalMovementSpeed);
		else
			navigate ('y');
	}

	public void stepDown(){
		if (Time.deltaTime * (selfPosition.y - 1) * diagonalMovementSpeed > -11.2f)
			baseCollision.Move (Time.deltaTime * new Vector2 (0, -1) * diagonalMovementSpeed);
		else
			navigate ('y');
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
		if (Time.deltaTime * selfPosition.y + deltaPosition.normalized.y * diagonalMovementSpeed < -0.1f)
			baseCollision.Move (Time.deltaTime * deltaPosition.normalized * diagonalMovementSpeed);
		else {
			// If I can't veer up, navigate using the y axis
			navigate ('y');
		}
	}

	public void veerDown(){
		if (playerPosition.x > selfPosition.x)
			deltaPosition = new Vector3 (1, -1, 0);
		else
			deltaPosition = new Vector3 (-1, -1, 0);
		// I can't veer down if I'm right above the bottom
		if (Time.deltaTime * selfPosition.y - deltaPosition.normalized.y * diagonalMovementSpeed > -11.2f)
			baseCollision.Move (Time.deltaTime * deltaPosition.normalized * diagonalMovementSpeed);
		else {
			// If I can't veer down, navigate using the y axis
			navigate ('y');
		}
	}

	/**
	 * Finds a way around the obstacle using the specified axis, when veering off won't work
	 */ 
	public void navigate(char axis){
		int i = 1;
		if (axis == 'y') {
			while (i < 10) { // gradually increase angle
				if (checkUp (i)) { // if I can move up at this angle
					newDirection = (direction + new Vector2 (0, i+1)).normalized;
					baseCollision.Move (Time.deltaTime * newDirection * diagonalMovementSpeed);
					//baseCollision.Move(Time.deltaTime * new Vector2(0, 1) * diagonalMovementSpeed);
					return;
				}
				if (checkDown (i)) { // if I can move down at this angle
					newDirection = (direction + new Vector2 (0, -i-1)).normalized;
					baseCollision.Move (Time.deltaTime * newDirection * diagonalMovementSpeed);
					//baseCollision.Move(Time.deltaTime * new Vector2(0, -1) * diagonalMovementSpeed);
					return;
				}
				i++;
			} // If I get here, I'm stuck.

			// TODO: Figure out how to get un-stuck. Only in NavigateObstacle.
			Debug.Log("stuck");
			//bailOut ();
		} else {
			while (i < 10) {
				if (checkRight (i)) { // if I can move right at this angle
					newDirection = (direction + new Vector2 (i+1, 0)).normalized;
					baseCollision.Move(Time.deltaTime * newDirection * diagonalMovementSpeed);
					//baseCollision.Move(Time.deltaTime * new Vector2(1, 0) * diagonalMovementSpeed);
					return;
				}
				if (checkLeft (i)) { // if I can move left at this angle
					newDirection = (direction + new Vector2 (-i-1, 0)).normalized;
					baseCollision.Move (Time.deltaTime * newDirection * diagonalMovementSpeed);
					//baseCollision.Move(Time.deltaTime * new Vector2(-1, 0) * diagonalMovementSpeed);
					return;
				}
				i++;
			} // If I get here, I'm stuck.
			Debug.Log("stuck");
			//bailOut ();
		}
	}

	/*public void bailOut(){
		Debug.Log ("Bailing");
		bailing = true;
		Vector2 perpendicular1 = new Vector2 (-directionNorm.x, -directionNorm.y * -1);
		Vector2 perpendicular2 = new Vector2 (-directionNorm.x * -1, -directionNorm.y);

		baseCollision.Move (Time.deltaTime * ((-directionNorm + perpendicular2)/2) * diagonalMovementSpeed);
	}*/


	public bool checkUpParallel(int i){
		if (Time.deltaTime * selfPosition.y + (2 * i) >= -0.1f)
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 2 * i), direction, 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, 2 * i), direction);
		if (hit && hit.collider.tag == "Obstacle")
			return false;
		return true;
	}

	public bool checkDownParallel(int i){
		if (Time.deltaTime * selfPosition.y - (2 * i) <= -11.2f)
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -2 * i), direction, 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, -2 * i), direction);
		if (hit && hit.collider.tag == "Obstacle")
			return false;
		return true;
	}

	public bool checkRightParallel(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (2 * i, 0), direction, 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (2 * i, 0), direction);
		if (hit && hit.collider.tag == "Obstacle")
			return false;
		return true;
	}

	public bool checkLeftParallel(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-2 * i, 0), direction, 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (-2 * i, 0), direction);
		if (hit && hit.collider.tag == "Obstacle")
			return false;
		return true;
	}

	/** 
	 //* checkDirection():
	 //*   Checks if the character can move in the specified direction
	 //*/ 

	public bool checkUp(int i){
		if (Time.deltaTime * selfPosition.y + i >= -0.1f) // can't go above the ground
			return false;
		// TODO: Mess with length = 2 and see if there's a better way?
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i), 2, layerMask); //length 2
		Debug.DrawRay(selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, 3), direction + new Vector2 (0, i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2(0, 3), direction + new Vector2 (0, i));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}

	public bool checkDown(int i){
		if (Time.deltaTime * selfPosition.y - i <= -11.2f) // can't go below the bottom
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, -3), direction + new Vector2(0, -i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2(0, -3), direction + new Vector2(0, -i));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}

	public bool checkRight(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}

	public bool checkLeft(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0));
		if (hit && hit.collider.tag=="Obstacle")
			return false;
		return true;
	}
}
