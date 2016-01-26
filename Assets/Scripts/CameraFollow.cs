using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float smoothTime = 0.15f;

    private Transform playerTransform;
    private float smoothVelX, smoothVelY;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate() {
        Vector3 pos = transform.position;

        pos.x = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref smoothVelX, smoothTime);
        pos.y = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref smoothVelY, smoothTime);

        transform.position = pos;
    }

}