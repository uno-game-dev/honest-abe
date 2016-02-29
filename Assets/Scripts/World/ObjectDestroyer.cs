using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    bool seen = false;

    void Start()
    {
    }

    void Update()
    {

        if (GetComponent<Renderer>().isVisible)
        {
            seen = true;
        }

        if (seen && !GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}