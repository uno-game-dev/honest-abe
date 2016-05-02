using UnityEngine;

public class Boss : MonoBehaviour
{
    public string bossName;

	private CameraFollow _cameraFollow;
	private Vector3 _playerPosition;
    private LevelManager _levelManager;
    private bool isMusicPlaying;

    // Use this for initialization
    void Start()
	{
		_cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        _levelManager = FindObjectOfType<LevelManager>();

        if (name.Contains("Bear"))
            SoundPlayer.Play("Bear Appearance");
        else if (name.Contains("Officer-Boss"))
            SoundPlayer.Play("Battlefield Boss Intro");
    }

    // Update is called once per frame
    void Update()
    {
		_playerPosition = GameObject.Find("Player").transform.position;
        if ((gameObject.transform.position.x - _playerPosition.x) < 10)
        {
			//The boss is in the scene with Abe so lock the camera
			_cameraFollow.lockRightEdge = true;
			GameObject.Find("UI").GetComponent<UIManager>().bossHealthUI.enabled = true;

            if (!isMusicPlaying)
            {
                isMusicPlaying = true;
                if (_levelManager.currentScene == 0)
                    MusicPlayer.Play("Forest Boss Music");
                else if (_levelManager.currentScene == 1)
                    MusicPlayer.Play("BattleField Boss Music");
                else if (_levelManager.currentScene == 2)
                    MusicPlayer.Play("Ballroom Boss Music Intro", "Ballroom Boss Music Loop");
            }
        }
    }
}
