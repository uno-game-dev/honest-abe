using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float smoothTime = 0.15f;

    private Transform playerTransform;
    private BoxCollider2D levelBounds;
    private Camera cam;
    private float smoothVelX, smoothVelY;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        levelBounds = GameObject.Find("_Manager").GetComponent<BoxCollider2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void LateUpdate() {
        Vector3 pos = transform.position;

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

        transform.position = pos;
    }

}