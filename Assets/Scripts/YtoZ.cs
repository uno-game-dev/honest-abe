using UnityEngine;
using System.Collections;

public class YtoZ : MonoBehaviour
{
    public float offsetFromOrigin = 2f;
    public float maxZ = 45;
    public float yToZFactor = 2f;

    private void Update()
    {
        ChangeZBasedOnY();
    }

    private void ChangeZBasedOnY()
    {
        Vector3 position = transform.position;
        position.z = -offsetFromOrigin + position.y * yToZFactor;
        position.z = Mathf.Clamp(position.z, -maxZ, 0);
        transform.position = position;
    }
}
