using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;
	new private Renderer renderer;

	private CameraFollow cameraFollow;


	void Start () {
		renderer = this.GetComponent<Renderer>();
		savedOffset = renderer.material.mainTextureOffset;
		cameraFollow = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ();

	}
	
	// Update is called once per frame
	void Update () {
		if ( cameraFollow.velocity.x > 0f && cameraFollow.velocity.magnitude > 0f) {
			float x = renderer.material.mainTextureOffset.x + (scrollSpeed * (cameraFollow.velocity.magnitude/-1000f));
			Vector2 offset = new Vector2 (x, savedOffset.y);
			renderer.material.mainTextureOffset = offset;
			Debug.Log (cameraFollow.velocity.magnitude);
		}
	}

	void OnDisable() {
		renderer.material.mainTextureOffset =  savedOffset;
	}
}
