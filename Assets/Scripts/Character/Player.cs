using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private static GameObject _instance;

	private PlayerHealth _playerHealth;

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
    }
	
	// Update is called once per frame
	void Update ()
	{

	}

	// Called before level transition to prevent camera bugs
	public void ResetPosition()
	{
		transform.position = new Vector3(-15, -2, 0);
	}
	
	// Runs when a scene is loaded
	public void Initialize()
	{
		Debug.Log("Initialize Player");
		_playerHealth.Initialize();
    }
}
