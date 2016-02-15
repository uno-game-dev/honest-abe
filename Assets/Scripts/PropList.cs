using UnityEngine;
using System.Collections;

public class PropList : MonoBehaviour {

	public GameObject rock;
    public GameObject shrub;

	private ArrayList props;

	// Use this for initialization
	void Start () {
		props = new ArrayList();
		props.Add(rock);
		props.Add(shrub);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetRandomProp() {
		System.Random rnd = new System.Random();
		var i = rnd.Next(0, props.Count);
		return props[i] as GameObject;
	}
}
