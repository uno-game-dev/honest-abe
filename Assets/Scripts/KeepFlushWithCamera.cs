using UnityEngine;
using System.Collections;

public class KeepFlushWithCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position.x , transform.position.y, transform.position.z);
	}
}
