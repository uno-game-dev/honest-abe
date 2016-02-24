using UnityEngine;
using System.Collections;

public class YtoZ : MonoBehaviour {
    public float yToZFactor = 2f;

    private void Update()
    {
        ChangeZBasedOnY();
    }

    private void ChangeZBasedOnY()
    {
        Vector3 localPosition = transform.localPosition;
        localPosition.z = localPosition.y * yToZFactor;
        transform.localPosition = localPosition;
    }
}
