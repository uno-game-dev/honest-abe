using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class checkCollisions : ConditionNode {

	public Vector2 velocity;
	private BaseCollision collision;
	private bool collidedWithObstacle = false;
	private Movement movement;

	override public void Start(){
		collision = self.GetComponent<BaseCollision>();
		movement = self.GetComponent<Movement> ();
		velocity = movement.velocity;
	}
/*
	override public void OnDisable()
	{
		collision.OnCollisionEnter -= OnCollision;
	}
*/
	private void OnCollision(Collider2D collider)
	{
		Debug.Log ("Called OnCollision");
		if (collider.tag == "Obstacle") {
			collidedWithObstacle = true;
			Debug.Log ("Collided with an obstacle.");
		}
	}

	// Update is called once per frame
	override public Status Update () {

		// If velocity is zero, check for collisions
		if (velocity.x == 0) {
			Debug.Log ("Checking for collisions");
			collision.OnCollisionEnter += OnCollision;
		} 
		if (collidedWithObstacle) {
			//movement.velocity = velocity * -1;	
			if (onSuccess.id != 0)
				owner.root.SendEvent (onSuccess.id);
			return Status.Success;
		} else {
			return Status.Failure;
		}	
	}
}
