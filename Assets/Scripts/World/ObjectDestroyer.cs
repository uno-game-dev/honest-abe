using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float distanceToDestroy = 32f;

    void Update()
    {
        if (Camera.main.transform.position.x - transform.position.x > distanceToDestroy)
            Destroy(gameObject);
    }
}