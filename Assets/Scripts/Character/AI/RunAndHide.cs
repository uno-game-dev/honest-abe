using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class RunAndHide : ActionNode {

	private GameObject[] obstacles; 
	private EnemyFollow enemyFollow;

	public override void Start () {
		// Find position of nearest obstacle, or bush whatever
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		enemyFollow = self.GetComponent<EnemyFollow> ();

		// Set enemyfollow target to that

		// But for now, just go towards the first obstacle
		enemyFollow.targetType = EnemyFollow.TargetType.TargetGameObject;
		enemyFollow.target = obstacles [0];
	}
	
	public override Status Update () {
		return Status.Success;
	}
}
