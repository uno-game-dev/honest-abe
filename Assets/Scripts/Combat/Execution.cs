using UnityEngine;
using System.Collections;

public class Execution : MonoBehaviour
{
    public GameObject healthUpPrefab;

    public void OnExecute(Vector3 position)
    {
        if (healthUpPrefab)
            Instantiate(healthUpPrefab, position, Quaternion.identity);
    }
}
