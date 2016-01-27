using UnityEngine;

public class CameraVelocity : MonoBehaviour
{
	// script to move camera
	public float speed = 7;

	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
	}
}