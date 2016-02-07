using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;
	new private Renderer renderer;

	// Use this for initialization
	void Start () {
		renderer = this.GetComponent<Renderer>();
		savedOffset = renderer.material.mainTextureOffset;

	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 ( x, savedOffset.y);
		renderer.material.mainTextureOffset = offset;
	}

	void OnDisable() {
		renderer.material.mainTextureOffset =  savedOffset;
	}
}
