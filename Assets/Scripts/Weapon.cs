using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public void OnCollision(GameObject other)
    {
        Debug.Log("Weapon Picked Up");
        Destroy(gameObject);
    }

}
