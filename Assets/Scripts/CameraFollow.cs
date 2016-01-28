using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float smoothTime = 0.15f;
    [HideInInspector] public float leftEdge;

    private Transform playerTransform, leftEdgeTransform;
    private BoxCollider2D levelBounds;
    private Camera cam;
    private float smoothVelX, smoothVelY;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        levelBounds = GameObject.Find("_Manager").GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();
        leftEdge = transform.position.x - (((2 * cam.orthographicSize) * cam.aspect) / 2);
        leftEdgeTransform = transform.GetChild(0);
    }

    void LateUpdate() {
        Vector3 pos = transform.position;

        leftEdge = pos.x - (((2 * cam.orthographicSize) * cam.aspect) / 2);
        leftEdgeTransform.position = new Vector3(leftEdge, leftEdgeTransform.position.y, leftEdgeTransform.position.z);

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
        if (pos.y + cam.orthographicSize > levelBounds.transform.position.y + (levelBounds.size.y / 2)) {
            pos.y = (levelBounds.transform.position.y + (levelBounds.size.y / 2) - cam.orthographicSize);
        }

        // Prevents the camera from going below the bounds of the level
        else if (pos.y - cam.orthographicSize < levelBounds.transform.position.y - (levelBounds.size.y / 2)) {
            pos.y = (levelBounds.transform.position.y - (levelBounds.size.y / 2) + cam.orthographicSize);
        }
        
        // Prevents the camera from going backwards
        if (pos.x - (((2 * cam.orthographicSize) * cam.aspect) / 2) < leftEdge) {
            pos.x = leftEdge + (((2 * cam.orthographicSize) * cam.aspect) / 2);
        }
        
        transform.position = pos;
    }

}