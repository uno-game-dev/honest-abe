using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private static GameObject _instance;
	
	private Movement _movement;
	private PlayerHealth _playerHealth;
	private PlayerMotor _playerMotor;
	private Vector2 _velocity;

	private bool _playEnding;

	void Awake()
	{
		if (_instance == null)
			_instance = gameObject;
		else if (_instance != gameObject)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start()
	{
		_playerHealth = GetComponent<PlayerHealth>();
		_playerMotor = GetComponent<PlayerMotor>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_playEnding)
		{
			_movement.Move(_velocity);
			transform.Translate(Vector3.right * (_movement.horizontalMovementSpeed / 4) * Time.deltaTime);
		}
	}

	// Runs when a scene is loaded
	public void Initialize()
	{
		Debug.Log("Initialize Player");
		transform.position = new Vector3(-15, -2, 0);
		if (transform.localScale.x < 0)
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		_playerHealth.Initialize();
		_playerMotor.Initialize();
	}

	public void PlayEnding()
	{
		GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = false;
		GetComponent<PlayerControls>().enabled = false;
		GetComponent<BaseCollision>().enabled = false;
		_playerMotor.enabled = false;
		_movement = GetComponent<Movement>();
		_velocity = new Vector2(_movement.horizontalMovementSpeed / 2, 0);
		Invoke("SetPlayEndingToTrue", 1);
	}

	public void SetPlayEndingToTrue()
	{
		_playEnding = true;
	}

}
