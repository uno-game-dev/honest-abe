using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {


	public float scrollSpeed;
    //Will only be used in instances where the script logic is disabled-then-reanabled
    private Vector2 savedOffset;
    //Local reference to the attatched GameObject's renderer
	new private Renderer renderer;
    //Ref to Main camera's Follow script
	private CameraFollow cameraFollow;


	void Start () {
		renderer = this.GetComponent<Renderer>();
		savedOffset = renderer.material.mainTextureOffset;
		cameraFollow = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ();

	}
	
	// Update is called once per frame
	void Update () {
        //We only want the background texture to move if the camera is moving forward
		if ( cameraFollow.velocity.x > 0f && cameraFollow.velocity.magnitude > 0f) {
            //Calculating how far backward along the x-axis the texture should move
            // Basically new-x = old-x + (scrollSpeed * (cameraspeed scaled down to miliseconds))
            float step = (scrollSpeed * (cameraFollow.velocity.magnitude * Time.deltaTime)) * -1f;
            float x = Mathf.Repeat(renderer.material.mainTextureOffset.x + step, 1);
           
            Vector2 offset = new Vector2 (x, savedOffset.y);
			renderer.material.mainTextureOffset = offset;
            Debug.LogFormat("X offset - {0}", x);
        }

        
        Debug.LogFormat("Texture offset - {0}",renderer.material.mainTextureOffset);
    }

    //Stores the current offset when this Behavior is disabled
	void OnDisable() {
		renderer.material.mainTextureOffset =  savedOffset;
	}
}
