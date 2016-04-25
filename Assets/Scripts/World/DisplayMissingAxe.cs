using UnityEngine;
using System.Collections;

public class DisplayMissingAxe : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (Transform child in transform)
		{
			if (transform.GetComponent<Perk>().type == PerkManager.activeAxePerk.type)
				child.gameObject.SetActive(true);
			else
				child.gameObject.SetActive(false);
		}
	}
}
