using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float smoothTime = 0.15f;
    [HideInInspector]
    public float leftEdge, rightEdge, bottomEdge;
    [HideInInspector]
    public Vector3 velocity, previousPos;
    public bool lockRightEdge = false;

    private Transform playerTransform, leftEdgeTransform, rightEdgeTransform, bottomEdgeTransform, bgBounds;
    private BoxCollider2D verticalBounds;
    private Camera cam;
    private float smoothVelX, smoothVelY, leftEdgeBoundsHalfWidth, bottomEdgeBoundsHalfHeight, rightEdgeBoundsHalfWidth;

    void Start() {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        verticalBounds = GameObject.Find("VerticalBounds").GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();

        bgBounds = GameObject.Find("BackgroundBounds").GetComponent<Transform>();
        bgBounds.localScale = new Vector3(((2 * cam.orthographicSize) * cam.aspect), 1, 1);

        leftEdge = transform.position.x - (((2 * cam.orthographicSize) * cam.aspect) / 2);
        leftEdgeTransform = transform.GetChild(0);
        leftEdgeBoundsHalfWidth = leftEdgeTransform.gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2;

        rightEdge = transform.position.x + (((2 * cam.orthographicSize) * cam.aspect) / 2);
        rightEdgeTransform = transform.GetChild(2);
        rightEdgeBoundsHalfWidth = rightEdgeTransform.gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2;

        bottomEdge = transform.position.y - cam.orthographicSize;
        bottomEdgeTransform = transform.GetChild(1);
        bottomEdgeBoundsHalfHeight = bottomEdgeTransform.gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2;

        Debug.Log(transform.position);

        // Initial positioning of the camera on top of the player
        // This prevents the "slide in" of the camera to the player when the game starts
        Vector3 pos = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        if (pos.y + cam.orthographicSize > verticalBounds.transform.position.y + (verticalBounds.size.y / 2))
            pos.y = (verticalBounds.transform.position.y + (verticalBounds.size.y / 2) - cam.orthographicSize);
        if (pos.y - cam.orthographicSize < verticalBounds.transform.position.y - (verticalBounds.size.y / 2))
            pos.y = (verticalBounds.transform.position.y - (verticalBounds.size.y / 2) + cam.orthographicSize);
        pos.x = (playerTransform.position.x - (playerTransform.gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2) - 2) + (((2 * cam.orthographicSize) * cam.aspect) / 2);
        transform.position = pos;
        
        Debug.Log(transform.position);
    }

    void LateUpdate() {
        previousPos = transform.position;
        Vector3 pos = transform.position;

        // Set the left edge collider to be the height of the camera and set it to be at the very left edge
        leftEdge = pos.x - (((2 * cam.orthographicSize) * cam.aspect) / 2);
        leftEdgeTransform.localScale = new Vector3(1, cam.orthographicSize * 2, 1);
        leftEdgeTransform.position = new Vector3(leftEdge - leftEdgeBoundsHalfWidth, leftEdgeTransform.position.y, leftEdgeTransform.position.z);

        // Set the right edge collider to be the height of the camera and set it to be at the very right edge
        rightEdge = pos.x + (((2 * cam.orthographicSize) * cam.aspect) / 2);
        rightEdgeTransform.localScale = new Vector3(1, cam.orthographicSize * 2, 1);
        rightEdgeTransform.position = new Vector3(rightEdge + rightEdgeBoundsHalfWidth, rightEdgeTransform.position.y, rightEdgeTransform.position.z);

        // Set the bottom edge to be as wide as the camera and keep it always set at the bottom
        bottomEdge = pos.y - cam.orthographicSize;
        bottomEdgeTransform.localScale = new Vector3(((2 * cam.orthographicSize) * cam.aspect), 1, 1);
        bottomEdgeTransform.position = new Vector3(bottomEdgeTransform.position.x, bottomEdge - bottomEdgeBoundsHalfHeight, bottomEdgeTransform.position.z);

        // The collider that prevents the player from walking onto the background
        bgBounds.localScale = new Vector3(((2 * cam.orthographicSize) * cam.aspect), 1, 1);
        bgBounds.position = new Vector3(pos.x, bgBounds.position.y, bgBounds.position.z);

        pos.x = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref smoothVelX, smoothTime);
        pos.y = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref smoothVelY, smoothTime);

        /* 
         * Use these to get camera size
         * cam.orthographicSize gives half of the height of the camera.
         * multiply it by 2 to get the full height.
         * multiplying that height by the aspect ratio gives you the width.
         * 
         * float height = 2f * cam.orthographicSize;
         * float width = height * cam.aspect;
         */

        // Prevents the camera from going above the bounds of the level
        if (pos.y + cam.orthographicSize > verticalBounds.transform.position.y + (verticalBounds.size.y / 2)) {
            pos.y = (verticalBounds.transform.position.y + (verticalBounds.size.y / 2) - cam.orthographicSize);
        }

        // Prevents the camera from going below the bounds of the level
        else if (pos.y - cam.orthographicSize < verticalBounds.transform.position.y - (verticalBounds.size.y / 2)) {
            pos.y = (verticalBounds.transform.position.y - (verticalBounds.size.y / 2) + cam.orthographicSize);
        }

        // Prevents the camera from going backwards
        if (pos.x - (((2 * cam.orthographicSize) * cam.aspect) / 2) < leftEdge) {
            pos.x = leftEdge + (((2 * cam.orthographicSize) * cam.aspect) / 2);
        }

        // If we want to lock the right edge
        if (lockRightEdge) {
            if (pos.x + (((2 * cam.orthographicSize) * cam.aspect) / 2) > rightEdge) {
                pos.x = rightEdge - (((2 * cam.orthographicSize) * cam.aspect) / 2);
            }
        }

        velocity.x = (pos.x - previousPos.x) / Time.deltaTime;
        velocity.y = (pos.y - previousPos.y) / Time.deltaTime;

        transform.position = pos;

    }

}