using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	private static GameObject _instance;

	void Awake ()
	{
		if (_instance == null)
			_instance = gameObject;
		else if (_instance != gameObject)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
