using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class NavigateObstacle : ConditionNode {

	// Definitely need
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

	// Probably don't need
	//private Vector2 newTarget;
	private Movement movement;
	//private GameObject[] obstacles;
	//private Vector3 cameraPosition;
	//private Vector3 bottomLeft;
	//private bool[,] grid;
	//private Stack<Node> path;
	//private Node nextStep;
	//private bool finishedStart = false;
	//private Vector3 topLeft = cameraPosition + new Vector3 (-20, 10, 0);
	//private Vector3 topRight = cameraPosition + new Vector3 (20, 10, 0);
	//private Vector3 bottomRight = cameraPosition + new Vector3 (20, -9, 0);

	/** TODO: 
	 * Maybe change the veers to check if the forward component is possible. 
	 *  If not, i.e. an obstacle is already in the way,
	 *  then just take the side step, without the forward component.
	 *  If I can't do that, then add the backward component
	 *  If I can't do that, then go in the exact opposite direction of the player?
	 */

	public void veerRight(){
		if (playerPosition.y > selfPosition.y)
			deltaPosition = new Vector2 (0.07f, 0.07f);
		else
			deltaPosition = new Vector2 (0.07f, -0.07f);
		self.transform.position = (Vector2) selfPosition + deltaPosition;
		Debug.Log ("veering right");
	}

	public void veerLeft(){
		if (playerPosition.y > selfPosition.y)
			deltaPosition = new Vector2 (-0.07f, 0.07f);
		else
			deltaPosition = new Vector2 (-0.07f, -0.07f);
		self.transform.position = (Vector2) selfPosition + deltaPosition;
		Debug.Log ("veering left");
	}

	public void veerUp(){
		if (selfPosition.y + 0.07f < 0) { // because they have to stay on the ground
			if (playerPosition.x > selfPosition.x)
				deltaPosition = new Vector2 (0.07f, 0.07f);
			else
				deltaPosition = new Vector2 (-0.07f, 0.07f);
			self.transform.position = (Vector2) selfPosition + deltaPosition;
			Debug.Log ("veering up");
		} else {
			veerDown ();
			Debug.Log ("couldn't veer up. Calling veerDown instead");
		}
	}

	public void veerDown(){
		if (playerPosition.x > selfPosition.x)
			deltaPosition = new Vector2 (0.07f, -0.07f);
		else
			deltaPosition = new Vector2 (-0.07f, -0.07f);
		self.transform.position = (Vector2) selfPosition + deltaPosition;
		Debug.Log ("veering down");
	}

	override public void Start(){

		// Definitely need these
		enemyFollow = self.GetComponent<EnemyFollow> ();
		enemyFollow.targetType = EnemyFollow.TargetType.Null;
		player = GameObject.Find("Player");
		selfPosition = self.transform.position;
		playerPosition = player.transform.position;
		direction = playerPosition - selfPosition;
		xDiff = Mathf.Abs (playerPosition.x - selfPosition.x);
		yDiff = Mathf.Abs (playerPosition.y - selfPosition.y);
		distanceToPlayer = Mathf.Sqrt (Mathf.Pow(xDiff,2) + Mathf.Pow(yDiff,2));
		layerMask = LayerMask.GetMask("Environment");

		// Not sure if I need these
		//enemyFollow.stopDistanceX = 0;
		movement = self.GetComponent<Movement> ();
		//direction = Vector2.Scale(direction, new Vector2(0.07f, 0.07f)); // scale direction vector, for DrawRay?
		//GameObject obj = new GameObject();

		// Fire 2 outer raycasts again and see which ones hit the obstacle
		if (xDiff > yDiff) {
			// Fire a ray above
			RaycastHit2D topHit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction, distanceToPlayer, layerMask);
			Debug.DrawRay (selfPosition + new Vector2 (0, 1), direction);
			// Fire a ray below
			RaycastHit2D bottomHit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction, distanceToPlayer, layerMask);
			Debug.DrawRay (selfPosition + new Vector2 (0, -1), direction);

			// If only the bottom one hits, veer up
			if (bottomHit && bottomHit.collider.tag=="Obstacle") {
				if (!(topHit && topHit.collider.tag=="Obstacle")) {
					veerUp ();
					return;
				} else {
					// If they both hit, navigate around the obstacle:
					int i = 1;
					while (i < 20) {
						if (checkUp (i)) {
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (0, i + 1), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating up");
							return;
						}
						if (checkDown (i)) {
							//newTarget = direction + new Vector2 (0, -i-1);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (0, -i - 1), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating down");
							return;
						}
						i++;
					}
				}
				// If only the top one hits, veer down
			} else {
				if (topHit && topHit.collider.tag=="Obstacle") {
					veerDown ();
					return;
				} else {
					// Should be clear, but you might be colliding with an obstacle. Copying nav code:
					Debug.Log("Should be clear.");
					int i = 1;
					while (i < 20) {
						if (checkUp (i)) {
							//newTarget = direction + new Vector2 (0, i+1);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (0, i + 1), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating up");
							return;
						}
						if (checkDown (i)) {
							//newTarget = direction + new Vector2 (0, -i-1);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (0, -i - 1), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating down");
							return;
						}
						i++;
					}
					Debug.Log ("checkUp and checkDown never worked.");
				}
			}

		} else if (yDiff > xDiff) {
			// Fire a ray to the right
			RaycastHit2D rightHit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction, distanceToPlayer, layerMask);
			Debug.DrawRay (selfPosition + new Vector2 (1, 0), direction);
			// Fire a ray to the left
			RaycastHit2D leftHit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction, distanceToPlayer, layerMask);
			Debug.DrawRay (selfPosition + new Vector2 (-1, 0), direction);

			// If only the left one hits, veer right
			if (leftHit && leftHit.collider.tag=="Obstacle") {
				if (!(rightHit && rightHit.collider.tag=="Obstacle")) {
					veerRight ();
					return;
				} else {
					// If they both hit, navigate around the obstacle:
					int i = 1;
					while (i < 20) {
						if (checkRight (i)) {
							//newTarget = direction + new Vector2 (i+1, 0);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (i + 1, 0), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating right");
							return;
						}
						if (checkLeft (i)) {
							//newTarget = direction + new Vector2 (-i-1, 0);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (-i - 1, 0), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating left");
							return;
						}
						i++;
					}
				}
				// If only the right one hits, veer left
			} else {
				if (rightHit && rightHit.collider.tag=="Obstacle") {
					veerLeft ();
					return;
				} else {
					// Should be clear, but you might be colliding with an obstacle. Copying nav code:
					Debug.Log ("Should be clear.");
					int i = 1;
					while (i < 20) {
						if (checkRight (i)) {
							//newTarget = direction + new Vector2 (i+1, 0);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (i + 1, 0), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating right");
							return;
						}
						if (checkLeft (i)) {
							//newTarget = direction + new Vector2 (-i-1, 0);
							deltaPosition = Vector2.ClampMagnitude(direction + new Vector2 (-i - 1, 0), 0.07f);
							self.transform.position = selfPosition + deltaPosition;
							Debug.Log ("navigating left");
							return;
						}
						i++;
					}
					Debug.Log ("checkRight and checkLeft never worked");
				}
			}

		} else {
			Debug.Log ("Diagonal");
		}



		/* AN OLDER APPROACH:

		// Get list of all obstacles
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");

		// Find the game world coordinates of the bottom left corner of the view
		cameraPosition = GameObject.Find ("Main Camera").transform.position;
		bottomLeft = cameraPosition + new Vector3 (-20, -9, 0);

		// Create a boolean grid to represent passable/unpassable locations
		grid = new bool[41, 21];
		for (int i = 0; i < 41; i++) {
			for (int j = 0; j < 21; j++) {
				grid [i, j] = true;
			}
		}

		// For each obstacle, get its position relative to the bottom left corner
		GameObject obstacle;
		for(int i=0; i<obstacles.Length; i++){
			obstacle = obstacles [i];
			int x = (int) obstacle.transform.position.x - (int) bottomLeft.x;
			int y = (int) obstacle.transform.position.y - (int) bottomLeft.y;
			Debug.Log ("Obstacle at " + x + ", " + y);

			// Set all locations within 2? units from an obstacle as unpassable
			for (int j=x-2; j<x+2; j++){
				for (int k=y-2; k<y+2; k++) {
					if(j >= 0 && j < 41 && k >= 0 && k < 21)
						grid [j, k] = false;
				}
			}
		}
			
		// Translate starting position and goal to grid space and create a Pathfinder
		Vector3 startingPosition = self.transform.position - bottomLeft;
		Vector3 goal = player.transform.position - bottomLeft;

		Debug.Log("Starting at "+Mathf.Round(startingPosition.x)+", "+Mathf.Round(startingPosition.y));
		Debug.Log ("Goal: "+Mathf.Round(goal.x)+", "+Mathf.Round(goal.y));
		Pathfinder pathfinder = new Pathfinder(startingPosition, goal, grid);

		// Search for the shortest path
		path = pathfinder.search();
		if(path!=null)
			nextStep = path.Pop ();

		if (path.Count == 0) {
			Debug.Log("Got back an empty path.");
			error = true;
		}

		// Take that step!
		Vector2 nextLocation = new Vector2 (bottomLeft.x + nextStep.x, bottomLeft.y + nextStep.y);
		self.GetComponent<Movement> ().Move (nextLocation);
		if ((Vector2) self.transform.position != nextLocation) {
			self.transform.position = nextLocation;
		}
		*/
	}

	// Update is called once per frame
	public override Status Update () {	
		//if (enemyFollow.targetType==EnemyFollow.TargetType.TargetGameObject && self.transform.position == enemyFollow.target.transform.position)
			//enemyFollow.targetType = EnemyFollow.TargetType.Player;
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
		//Debug.Log ("Called Update.");

		//if (error)
		//	return Status.Error;

		/* TESTING THIS:
		if (path != null) {
			if (path.Count == 0) {
				//if (onSuccess.id != 0)
					//owner.root.SendEvent(onSuccess.id);
				//return Status.Success;
			} else {
				//Node nextStep = path.Pop ();
				//path.Clear ();
				Debug.Log ("Taking step: "+nextStep.x+", "+nextStep.y);
				Vector2 nextLocation = new Vector2 (bottomLeft.x + nextStep.x, bottomLeft.y + nextStep.y);
				self.GetComponent<Movement> ().Move (nextLocation);
				if ((Vector2) self.transform.position != nextLocation) {
					self.transform.position = nextLocation;
				}
			}
		}*/

		// Find dx and dy, distances between me and player
		// If dx > dy, loop for i: 
		// Check raycast with y+i
			// If it's free, Move in that direction
		// Check with y-i

		//return Status.Running;

		/* This sort of works but it's glitchy and not good enough.
		if (Mathf.Abs (GameObject.Find ("Player").transform.position.y - self.transform.position.y) >
		    Mathf.Abs (GameObject.Find ("Player").transform.position.x - self.transform.position.x)) {
			if (Physics2D.Raycast (self.transform.position, new Vector2 (-1, 0), 2, LayerMask.GetMask ("Environment"))) {
				self.GetComponent<Movement> ().Move (new Vector2 (5, 0));
			} else {
				self.GetComponent<Movement> ().Move (new Vector2 (-5, 0));
			}
		} else {
			if (Physics2D.Raycast (self.transform.position, new Vector2 (0, 1), 2, LayerMask.GetMask ("Environment"))) {
				self.GetComponent<Movement> ().Move (new Vector2 (0, -5));
			} else {
				self.GetComponent<Movement> ().Move (new Vector2 (0, 5));
			}
		}*/


		//GameObject obstacleInTheWay = null;
		/*foreach (GameObject obstacle in obstacles) {
			if (vectorIsBetween (obstacle.transform.position, self.transform.position, GameObject.Find ("Player").transform.position)) {
				obstacleInTheWay = obstacle;
			}
		}*/

		/*
		if (obstacleInTheWay != null) {
			// If y is the greater distance, find a spot just to the left or right of the obstacle and go there.
			// Left/right depends on which side is closer to let's say me.
			GameObject obj = new GameObject();
			if (Mathf.Abs (GameObject.Find ("Player").transform.position.y - self.transform.position.y) >
				Mathf.Abs (GameObject.Find ("Player").transform.position.x - self.transform.position.x)) {
				obj.transform.position = obstacleInTheWay.transform.position + new Vector3 (-2, 0, 0);
			} else {
				// If x is the greater distance, find a spot just above or below the obstacle and go there.
				obj.transform.position = obstacleInTheWay.transform.position + new Vector3 (0, 2, 0);
			}

			self.GetComponent<EnemyFollow> ().targetType = EnemyFollow.TargetType.TargetGameObject;
			self.GetComponent<EnemyFollow> ().target = obj;
		}*/
	}

	/* 
	 * These functions are called up to 20 times, alternating either between Up and Down, or between Left and Right, 
	 *  with i increasing after calling both. (i starts at 1)
	 * 	
	 * Distance param of these hits needs to be slightly more than the distance between you and obstacle
	 */

	// Fires the uppermost ray one step (of size i) up from the original direction. 
	// If it hits, fires another ray 2 up from that. Returns true if it does NOT hit.
	public bool checkUp(int i){
		if (selfPosition.y + i + 1 >= 0) // can't go above the ground
			return false;
		//length 2
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i), 2, layerMask);
		//length of direction
		Debug.DrawRay (selfPosition + new Vector2 (0, 1), direction + new Vector2 (0, i));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		// Imagine this as the new bottom ray, and fire the corresponding top ray.
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, 3), direction + new Vector2 (0, i), 2, layerMask);
		if (hit && hit.collider.tag == "Obstacle") {
			return false;
		}
		return true;
	}

	// Fires the bottommost ray one step (of size i) down from the original direction. 
	// If it hits, fires another ray 2 down form that. Returns true if it does NOT hit.
	public bool checkDown(int i){
		//length 2
		hit = Physics2D.Raycast (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i), 2, layerMask);
		//length of direction
		Debug.DrawRay (selfPosition + new Vector2 (0, -1), direction + new Vector2 (0, -i));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		// Imagine this as the new top ray, and fire the corresponding bottom ray.
		hit = Physics2D.Raycast(selfPosition + new Vector2(0, -3), direction + new Vector2(0, -i), 2, layerMask);
		if (hit && hit.collider.tag == "Obstacle") {
			return false;
		}
		return true;
	}

	// Fires the rightmost ray one step (of size i) to the right of the original direction.
	// If it hits, fires another ray 2 to the right of that. Returns true if it does NOT hit.
	public bool checkRight(int i){
		//length 2
		hit = Physics2D.Raycast (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0), 2, layerMask);
		//length of direction
		Debug.DrawRay (selfPosition + new Vector2 (1, 0), direction + new Vector2 (i, 0));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		// Imagine this as the new left ray, and fire the corresponding right ray
		hit = Physics2D.Raycast (selfPosition + new Vector2 (3, 0), direction + new Vector2 (i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		return true;
	}

	// Fires the leftmost ray one step (of size i) to the left of the original direction.
	// If it hits, fires another ray 2 to the left of that. Returns true if it does NOT hit.
	public bool checkLeft(int i){
		//length 2
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		//length of direction
		Debug.DrawRay (selfPosition + new Vector2 (-1, 0), direction + new Vector2 (-i, 0));
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		// Imagine this as the new right ray, and fire the corresponding left ray
		hit = Physics2D.Raycast (selfPosition + new Vector2 (-3, 0), direction + new Vector2 (-i, 0), 2, layerMask);
		if (hit && hit.collider.tag=="Obstacle"){
			return false;
		}
		return true;
	}


	/* Returns true if vector A is between vectors B and C
	public bool vectorIsBetween(Vector3 a, Vector3 b, Vector3 c){
		if (Mathf.Abs (a.x - c.x) < Mathf.Abs (b.x - c.x) && Mathf.Abs (a.x - b.x) < Mathf.Abs (b.x - c.x)) {
			if(Mathf.Abs (a.y - c.y) < Mathf.Abs (b.y - c.y) && Mathf.Abs (a.y - b.y) < Mathf.Abs (b.y - c.y)){
				return true;
			}
		}
		return false;
	}*/

}
