using UnityEngine;

public class Boss : MonoBehaviour
{
	private CameraFollow _cameraFollow;
	private Vector3 _playerPosition;
	private Health _health;
	private GameManager _gameManager;

    // Use this for initialization
    void Start()
	{
		_cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
		_playerPosition = GameObject.Find("Player").transform.position;
        if ((gameObject.transform.position.x - _playerPosition.x) < 10)
        {
			//The boss is in the scene with Abe so lock the camera
			_cameraFollow.lockRightEdge = true;
		}
		if (!_health.alive)
		{
			_gameManager.win = true;
		}
    }
}
