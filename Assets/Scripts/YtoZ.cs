using UnityEngine;
using System.Collections;

public class YtoZ : MonoBehaviour {
    public float offsetFromOrigin = 2f;
    public float maxZ = 45;
    public float yToZFactor = 2f;

    private void Update()
    {
        ChangeZBasedOnY();
    }

    private void ChangeZBasedOnY()
    {
        Vector3 localPosition = transform.localPosition;
        localPosition.z = -offsetFromOrigin + localPosition.y * yToZFactor;
        localPosition.z = Mathf.Clamp(localPosition.z, -maxZ, 0);
        transform.localPosition = localPosition;
    }
}
