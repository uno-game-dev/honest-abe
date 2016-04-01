using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class NavigateObstacle : ConditionNode {

	private GameObject player;
	private Vector2 selfPosition;
	private Vector2 playerPosition;
	private Vector2 direction;
	private EnemyFollow enemyFollow;
	private float xDiff;
	private float yDiff;
	private float distanceToPlayer;
	private LayerMask layerMask;
	private RaycastHit2D hit;
	private Vector2 deltaPosition;
	private BaseCollision baseCollision;

	override public void Start(){
		player = GameObject.Find ("Player");
		enemyFollow = self.GetComponent<EnemyFollow> ();
		layerMask = LayerMask.GetMask("Environment");
		baseCollision = self.GetComponent<BaseCollision> ();

		// Stop following
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
	}

	public override Status Update () {	

		// Calculate direction and distance to player
		selfPosition = self.transform.position;
		playerPosition = player.transform.position;
		direction = playerPosition - selfPosition;
		xDiff = Mathf.Abs (playerPosition.x - selfPosition.x);
		yDiff = Mathf.Abs (playerPosition.y - selfPosition.y);
		distanceToPlayer = Mathf.Sqrt (Mathf.Pow(xDiff,2) + Mathf.Pow(yDiff,2));

		// Fire 2 raycasts at the player and see which ones hit the obstacle
		if (xDiff > yDiff) {
			RaycastHit2D topHit = Physics2D.Raycast (selfPosition + new Vector2 (0, 0.5f), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, 0.5f), direction);
			RaycastHit2D bottomHit = Physics2D.Raycast (selfPosition + new Vector2 (0, -0.5f), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, -0.5f), direction);

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
			RaycastHit2D rightHit = Physics2D.Raycast (selfPosition + new Vector2 (0.5f, 0), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0.5f, 0), direction);
			RaycastHit2D leftHit = Physics2D.Raycast (selfPosition + new Vector2 (-0.5f, 0), direction, distanceToPlayer, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (-0.5f, 0), direction);

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

	// Finds a way around the obstacle using the specified axis
	public void navigate(char axis){
		int i = 1;
		if (axis == 'y') {
			while (i < 20) {
				if (checkUp (i)) { 
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (0, i + 1, 0), 0.07f);
					baseCollision.Move (deltaPosition);
					return;
				}
				if (checkDown (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (0, -i - 1, 0), 0.07f);
					baseCollision.Move(deltaPosition);
					return;
				}
				i++;
			}
			// If I get here, I'm stuck.
		} else {
			while (i < 20) {
				if (checkRight (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (i + 1, 0, 0), 0.07f);
					baseCollision.Move(deltaPosition);
					return;
				}
				if (checkLeft (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (-i - 1, 0, 0), 0.07f);
					baseCollision.Move(deltaPosition);
					return;
				}
				i++;
			}
			// If I get here, I'm stuck.
		}
	}

	public void veerUp(){
		// I can't veer up if I'm right below the skyline
		if (selfPosition.y + 0.07f < -0.1f) {
			if (playerPosition.x > selfPosition.x) {
				deltaPosition = new Vector3 (0.07f, 0.07f, 0);
			} else {
				deltaPosition = new Vector3 (-0.07f, 0.07f, 0);
			}
			baseCollision.Move (deltaPosition);
		} else { 
			// If I can't veer up, navigate using the y axis
			navigate('y');
		}
	}

	public void veerDown(){
		if (playerPosition.x > selfPosition.x)
			deltaPosition = new Vector3 (0.07f, -0.07f, 0);
		else
			deltaPosition = new Vector3 (-0.07f, -0.07f, 0);
		baseCollision.Move (deltaPosition);
	}

	public void veerRight(){
		if (playerPosition.y > selfPosition.y) {
			deltaPosition = new Vector3 (0.07f, 0.07f, 0);
		} else {
			deltaPosition = new Vector3 (0.07f, -0.07f, 0);
		}
		baseCollision.Move (deltaPosition);
	}

	public void veerLeft(){
		if (playerPosition.y > selfPosition.y)
			deltaPosition = new Vector3 (-0.07f, 0.07f, 0);
		else
			deltaPosition = new Vector3 (-0.07f, -0.07f, 0);
		baseCollision.Move (deltaPosition);

	}

	// Checks if the next upward angle is clear
	public bool checkUp(int i){
		if (selfPosition.y + i + 1 >= -0.1f) // can't go above the ground
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i), 2, layerMask); //length 2
		Debug.DrawRay (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i)); //length of direction
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, 3), direction + new Vector2 (0, i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, 3), direction + new Vector2 (0, i)); //length of direction
		if (hit && hit.collider.tag=="Obstacle") {
			return false;
		}
		return true;
	}

	// Checks if the next downward angle is clear
	public bool checkDown(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i));
		if (hit && hit.collider.tag=="Obstacle") {
			return false;
		}
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, -3), direction + new Vector2(0, -i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, -3), direction + new Vector2 (0, -i));
		if (hit && hit.collider.tag=="Obstacle") {
			return false;
		}
		return true;
	}

	// Checks if the next rightward angle is clear
	public bool checkRight(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		hit = Physics2D.Raycast (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		return true;
	}

	// Checks if the next leftward angle is clear
	public bool checkLeft(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		return true;
	}
}
