using UnityEngine;

/*
    Manages a scrolling texture on a game object
    In order for this script to function, the game object it is attatched to MUST
       also have as components:
        - A Quad Mesh Filter (Any mesh may suffice, but a Quad is expected)
        - A Mesh Renderer
        - A texture material
*/
public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;

    //Will only be used in instances where the script logic is disabled-then-reanabled
    private Vector2 savedOffset;
    //Local reference to the attatched GameObject's renderer
    new private Renderer renderer;
    public Direction scrollDirection = Direction.Right;

    //Value used to determine direction of scrolling. Should only ever be -1f, 0f, or 1f
    private float directionScalar = 0f;
    //Enum to abstract out Scrolling directions
    [HideInInspector]
    public enum Direction { None, Left, Right };

    //Ref to Main camera's Follow script
    private CameraFollow cameraFollow;
    
    void Awake()
    {
        renderer = this.GetComponent<Renderer>();
        savedOffset = renderer.material.mainTextureOffset;
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        //IMPORTANT: Scrolling direction is determined at component Awake time!
        // Changing the value of scrolldirection after GameObject is initialized will have NO effect by design.
        if (scrollDirection != Direction.None)
            directionScalar = scrollDirection == Direction.Right ? -1f : 1f;

    }

    // Update is called once per frame
    void Update()
    {
        //We only want the background texture to move if the camera is moving forward
        if (cameraFollow.velocity.x > 0f)
        {
            //Calculate how far backward or forward along the x-axis the texture should move
            // Basically new-x = old-x + (scrollSpeed * (cameraspeed scaled with deltatime))
            float step = (scrollSpeed * (cameraFollow.velocity.x * Time.deltaTime)) * directionScalar * renderer.material.mainTextureScale.x;
            float x = Mathf.Repeat(renderer.material.mainTextureOffset.x  + step, 1) ;

            Vector2 offset = new Vector2(x, savedOffset.y);

            if (!float.IsNaN(offset.x) && !float.IsNaN(offset.y))
                renderer.material.mainTextureOffset = offset;
        }
    }

    //Stores the current offset when this Behavior is disabled
    void OnDisable()
    {
        renderer.material.mainTextureOffset = savedOffset;
    }
}
