using UnityEngine;
using System.Collections;

public class FloatUpAndDestroy : MonoBehaviour
{
    public float floatTimer = 0;
    public float destroyIn = 1;
    public float floatVelocity = 4;
    public float floatGravity = -9.81f;
    public float floatGravityMultiplier = 1;

    void Start()
    {
        Destroy(gameObject, destroyIn);
    }

    void Update()
    {
        transform.Translate(0, floatVelocity * Time.deltaTime, 0);
        floatVelocity += floatGravity * floatGravityMultiplier * Time.deltaTime;
    }
}
