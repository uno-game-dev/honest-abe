using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour
{

    bool seen = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame

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