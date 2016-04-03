using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private static GameObject _instance;

	private PlayerHealth _playerHealth;
	private PlayerMotor _playerMotor;

	void Awake ()
	{
		if (_instance == null)
			_instance = gameObject;
		else if (_instance != gameObject)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start ()
	{ 
		_playerHealth = GetComponent<PlayerHealth>();
		_playerMotor = GetComponent<PlayerMotor>();
    }
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	// Runs when a scene is loaded
	public void Initialize()
	{
		Debug.Log("Initialize Player");
		transform.position = new Vector3(-15, -2, 0);
		_playerHealth.Initialize();
		_playerMotor.Initialize();
    }
}
