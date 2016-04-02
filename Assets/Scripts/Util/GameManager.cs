using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool perkChosen;

    private static GameObject _instance;

    private CameraFollow _cameraFollow;
    private LevelManager _levelManager;

    void Awake()
    {
        if (_instance == null)
            _instance = gameObject;
        else if (_instance != gameObject)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _levelManager = GetComponent<LevelManager>();
    }

	void Update()
    {
        if (GlobalSettings.currentSceneIsNew)
            Initialize();
        if (!perkChosen)
            _cameraFollow.lockRightEdge = true;
        else
            _cameraFollow.lockRightEdge = false;
	}


	// Runs when a scene is loaded
	private void Initialize()
	{
		GameObject.Find("Player").GetComponent<Player>().Initialize();
		_cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		GlobalSettings.currentSceneIsNew = false;
    }

	public void Win()
	{
		PerkManager.UpdatePerkStatus(GlobalSettings.axe_dtVampirism_name, 1);
        _levelManager.loadNextLevel();
	}
}