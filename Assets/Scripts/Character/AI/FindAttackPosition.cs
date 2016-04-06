using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class FindAttackPosition : ActionNode {

	private GameObject player;
	private EnemyFollow enemyFollow;
	private Vector3[] rightPositions;
	private Vector3[] leftPositions;
	private GameObject target;
	private Vector3 selfPosition;
	private Vector3 playerPosition;

	public override void Start () {

		player = GameObject.Find ("Player");
		enemyFollow = self.GetComponent<EnemyFollow> ();

		// Find my target or create one
		if (enemyFollow.target == null)
			target = new GameObject ();
		else
			target = enemyFollow.target;

		// Define attack positions (offsets from the player's position)
		rightPositions = new Vector3[] {
			new Vector3 (1, 0, 0), // Rpos0: right
			new Vector3 (1, 1f, 0), // Rpos1: right & up
			new Vector3 (1, -1f, 0), // Rpos2: right & down
			new Vector3 (2f, 0, 0), // Rpos3: right 2x
			new Vector3 (2f, 1f, 0), // Rpos4: right 2x & up
			new Vector3 (2f, -1f, 0) // Rpos5: right 2x & down
		};
		leftPositions = new Vector3[]{
			new Vector3(-1, 0, 0), // Lpos0: left
			new Vector3(-1, 1f, 0), // Lpos1: left & up
			new Vector3(-1, -1f, 0), // Lpos2: left & down
			new Vector3(-2f, 0, 0), // Lpos3: left 2x
			new Vector3(-2f, 1f, 0), // Lpos4: left 2x & up
			new Vector3(-2f, -1f, 0) // Lpos5: left 2x & down
		};
	}

	public override Status Update () {

		// Unclaim my attack position, since I'm not there yet
		float attackPosition = blackboard.GetFloatVar("attackPosition");
		if (attackPosition != -1) {
			string side = blackboard.GetStringVar ("attackSide");
			GlobalBlackboard.Instance.GetBoolVar (side + "pos" + attackPosition).Value = false;
			blackboard.GetFloatVar ("attackPosition").Value = -1;
			blackboard.GetStringVar ("attackSide").Value = null;
		}

		// Figure out which side of the Player I'm on
		selfPosition = self.transform.position;
		playerPosition = player.transform.position;
		string mySide, otherSide;
		if (selfPosition.x - playerPosition.x > 0) {
			mySide = "R";
			otherSide = "L";
		} else {
			mySide = "L";
			otherSide = "R";
		}

		// Find the best available position around Abe
		for (int i = 0; i <= 6; i++) { // Loop up to 6 even though positions only go up to 5
			// If i is a valid position number, check my side first, then the other
			if (i != 6) { 
				if (!GlobalBlackboard.Instance.GetBoolVar (mySide + "pos" + i)) {
					claimPosition(mySide, i);
					break;
				} else if (!GlobalBlackboard.Instance.GetBoolVar(otherSide + "pos" + i)){
					claimPosition (otherSide, i);
					break;
				}
				// If all the positions are full, put my target at the closest 0 position anyway, but don't claim it
			} else followPosition(mySide, 0);
		}

		return Status.Running;
	}

	// Claim the position and take it
	public void claimPosition(string side, int i){
		GlobalBlackboard.Instance.GetBoolVar (side + "pos" + i).Value = true;
		blackboard.GetFloatVar ("attackPosition").Value = i;
		blackboard.GetStringVar ("attackSide").Value = side;
		followPosition (side, i);
	}

	// Take the position
	public void followPosition(string side, int i){
		// Put my target at that position
		if (side == "R")
			target.transform.position = player.transform.position + rightPositions [i];
		else
			target.transform.position = player.transform.position + leftPositions [i];
		enemyFollow.targetType = EnemyFollow.TargetType.TargetGameObject;
		enemyFollow.target = target;
		// Change my stop distance to 0 so that I go all the way to that spot
		enemyFollow.stopDistanceX = 0;
		enemyFollow.stopDistanceY = 0;
	}
}
