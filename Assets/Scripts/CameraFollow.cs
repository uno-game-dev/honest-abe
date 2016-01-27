using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float smoothTime = 0.15f;

    private Transform playerTransform;
    private BoxCollider2D levelBounds;
    private Camera cam;
    private float smoothVelX, smoothVelY;
    private float leftEdge;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        levelBounds = GameObject.Find("_Manager").GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();
        leftEdge = transform.position.x - (((2 * cam.orthographicSize) * cam.aspect) / 2);
    }

    void LateUpdate() {
        Vector3 pos = transform.position;

        leftEdge = pos.x - (((2 * cam.orthographicSize) * cam.aspect) / 2);

        pos.x = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref smoothVelX, smoothTime);
        pos.y = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref smoothVelY, smoothTime);

        // Getting camera size
        //float height = 2f * cam.orthographicSize;
        //float width = height * cam.aspect;

        if (pos.y + cam.orthographicSize > levelBounds.transform.position.y + (levelBounds.size.y / 2))
        {
            pos.y = (levelBounds.transform.position.y + (levelBounds.size.y / 2) - cam.orthographicSize);
        }
        else if (pos.y - cam.orthographicSize < levelBounds.transform.position.y - (levelBounds.size.y / 2))
        {
            pos.y = (levelBounds.transform.position.y - (levelBounds.size.y / 2) + cam.orthographicSize);
        }

        if (pos.x - (((2 * cam.orthographicSize) * cam.aspect) / 2) < leftEdge)
        {
            pos.x = leftEdge;
        }

        transform.position = pos;
    }

}