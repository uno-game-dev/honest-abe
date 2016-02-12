using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {

	public float scrollSpeed;
	public Direction scrollDirection = Direction.Right;

	private Vector2 savedOffset;
	new private Renderer renderer;
	private float directionScalar = 0f;
	[HideInInspector]public enum Direction {None, Left, Right };
	private CameraFollow cameraFollow;


	void Start () {
		renderer = this.GetComponent<Renderer>();
		savedOffset = renderer.material.mainTextureOffset;
		cameraFollow = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ();
		if( scrollDirection != Direction.None)
			directionScalar = scrollDirection == Direction.Right ? 1f : -1f; 

	}
	
	// Update is called once per frame
	void Update () {
		if ( cameraFollow.velocity.x > 0f && cameraFollow.velocity.magnitude > 0f) {
			float x = renderer.material.mainTextureOffset.x + (scrollSpeed * (cameraFollow.velocity.magnitude * Time.deltaTime) * directionScalar);
			Vector2 offset = new Vector2 (x, savedOffset.y);
			renderer.material.mainTextureOffset = offset;
			Debug.Log (cameraFollow.velocity.magnitude);
		}
	}

	void OnDisable() {
		renderer.material.mainTextureOffset =  savedOffset;
	}
}
