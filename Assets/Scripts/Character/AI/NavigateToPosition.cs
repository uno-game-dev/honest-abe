using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class NavigateToPosition : ConditionNode {

	private GameObject target;
	private EnemyFollow enemyFollow;
	private Vector2 selfPosition;
	private Vector2 targetPosition;
	private Vector2 direction;
	private float xDiff;
	private float yDiff;
	private float distanceToTarget;
	private LayerMask layerMask;
	private BaseCollision baseCollision;
	private Vector2 deltaPosition;
	private RaycastHit2D hit;

	public override void Start () {	
		enemyFollow = self.GetComponent<EnemyFollow> ();
		baseCollision = self.GetComponent<BaseCollision> ();
		layerMask = LayerMask.GetMask("Player");

		// Find my target and stop following it
		target = enemyFollow.target;
		if (target == null)
			Debug.Log ("Error: Called NavigateToPosition without a target position");
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
	}
	
	public override Status Update () {

		// Calculate direction and distance to target
		selfPosition = self.transform.position;
		targetPosition = target.transform.position;
		direction = targetPosition - selfPosition;
		xDiff = Mathf.Abs (targetPosition.x - selfPosition.x);
		yDiff = Mathf.Abs (targetPosition.y - selfPosition.y);
		distanceToTarget = Mathf.Sqrt (Mathf.Pow(xDiff,2) + Mathf.Pow(yDiff,2));

		// Fire 2 raycasts at the target and see which one hits the player
		if (xDiff > yDiff) {
			RaycastHit2D topHit = Physics2D.Raycast (selfPosition + new Vector2 (0, .5f), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, .5f), direction);
			RaycastHit2D bottomHit = Physics2D.Raycast (selfPosition + new Vector2 (0, -.5f), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (0, -.5f), direction);

			// If only the bottom one hits, veer up
			if (bottomHit && bottomHit.collider.tag=="Player") {
				if (!(topHit && topHit.collider.tag == "Player")) {
					veerUp ();
					return Status.Running;
				} else navigate ('y'); // If they both hit, navigate around the obstacle
				// If only the top one hits, veer down
			} else {
				if (topHit && topHit.collider.tag == "Player") {
					veerDown ();
					return Status.Running;
				}
			}
		} else {
			RaycastHit2D rightHit = Physics2D.Raycast (selfPosition + new Vector2 (.5f, 0), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (.5f, 0), direction);
			RaycastHit2D leftHit = Physics2D.Raycast (selfPosition + new Vector2 (-.5f, 0), direction, distanceToTarget, layerMask);
				Debug.DrawRay (selfPosition + new Vector2 (-.5f, 0), direction);

			// If only the left one hits, veer right
			if (leftHit && leftHit.collider.tag=="Player") {
				if (!(rightHit && rightHit.collider.tag=="Player")) {
					veerRight ();
					return Status.Running;
				} else navigate('x'); // If they both hit, navigate around the obstacle
			} else {
				// If only the right one hits, veer left
				if (rightHit && rightHit.collider.tag=="Player" ) {
					veerLeft ();
					return Status.Running;
				}
			}
		}

		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}

	// Finds a way around the player using the specified axis
	public void navigate(char axis){
		int i = 1;
		if (axis == 'y') {
			while (i < 20) {
				if (checkUp (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (0, i + 1, 0), 0.1f);
					baseCollision.Move (deltaPosition);
					return;
				}
				if (checkDown (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (0, -i - 1, 0), 0.1f);
					baseCollision.Move (deltaPosition);
					return;
				}
				i++;
			}
		} else {
			while (i < 20) {
				if (checkRight (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (i + 1, 0, 0), 0.1f);
					baseCollision.Move (deltaPosition);
					return;
				}
				if (checkLeft (i)) {
					deltaPosition = Vector3.ClampMagnitude((Vector3) direction + new Vector3 (-i - 1, 0, 0), 0.1f);
					baseCollision.Move (deltaPosition);
					return;
				}
				i++;
			}
		}
	}

	public void veerUp(){
		// I can't veer up if I'm right below the skyline
		if (selfPosition.y + 0.07f < -0.1f) { 
			if (targetPosition.x > selfPosition.x) {
				deltaPosition = new Vector3 (.07f, .07f, 0);
			} else {
				deltaPosition = new Vector3 (-.07f, .07f, 0);
			}
			baseCollision.Move (deltaPosition);
		} else {
			// If I can't veer up, navigate using the y axis
			navigate ('y');
		}
	}

	public void veerDown(){
		// I can't veer down if I'm right above the bottom
		if (selfPosition.y - 0.07f > -11.2f) {
			if (targetPosition.x > selfPosition.x)
				deltaPosition = new Vector3 (.07f, -.07f, 0);
			else
				deltaPosition = new Vector3 (-.07f, -.07f, 0);
			baseCollision.Move (deltaPosition);
		} else {
			// If I can't veer down, navigate using the y axis
			navigate ('y');
		}
	}

	public void veerRight(){
		if (targetPosition.y > selfPosition.y) {
			deltaPosition = new Vector3 (.07f, .07f, 0);
		} else {
			deltaPosition = new Vector3 (.07f, -.07f, 0);
		}
		baseCollision.Move (deltaPosition);
	}

	public void veerLeft(){
		if (targetPosition.y > selfPosition.y)
			deltaPosition = new Vector3 (-.07f, .07f, 0);
		else
			deltaPosition = new Vector3 (-.07f, -.07f, 0);
		baseCollision.Move (deltaPosition);

	}

	// Checks if the next upward angle is clear
	public bool checkUp(int i){
		if (selfPosition.y + i + 1 >= -0.1f) // can't go above the ground
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i), 2, layerMask); //length 2
		Debug.DrawRay (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i)); //length of direction
		if (hit && hit.collider.tag=="Player"){
			return false;
		}
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, 3), direction + new Vector2 (0, i), 2, layerMask);
		if (hit && hit.collider.tag=="Player") {
			return false;
		}
		return true;
	}

	// Checks if the next downward angle is clear
	public bool checkDown(int i){
		if (selfPosition.y - i - 1 <= -11.2f) // can't go below the bottom
			return false;
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i));
		if (hit && hit.collider.tag=="Player") {
			return false;
		}
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, -3), direction + new Vector2(0, -i), 2, layerMask);
		if (hit && hit.collider.tag=="Player") {
			return false;
		}
		return true;
	}

	// Checks if the next rightward angle is clear
	public bool checkRight(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0));
		if (hit && hit.collider.tag=="Player"){
			return false;
		}
		hit = Physics2D.Raycast (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Player"){
			return false;
		}
		return true;
	}

	// Checks if the next leftward angle is clear
	public bool checkLeft(int i){
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		Debug.DrawRay (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0));
		if (hit && hit.collider.tag=="Player"){
			return false;
		}
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Player"){
			return false;
		}
		return true;
	}


}
