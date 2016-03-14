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
        CheckIfLost();
		CheckIfWon();
	}


    // Runs when a scene is loaded
    public void Initialize()
    {
        _cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        perkChosen = false;
        GlobalSettings.loseCondition = false;
        GlobalSettings.winCondition = false;
        GlobalSettings.currentSceneIsNew = false;
    }

	public void CheckIfWon()
	{
		//Checks if the boss health is 0 -- for alpha
		if (GlobalSettings.winCondition)
		{
			GlobalSettings.winCondition = false;
			PerkManager.UpdatePerkStatus(GlobalSettings.axe_dtVampirism_name, 1);
            _levelManager.loadNextLevel();
		}
	}

	public void CheckIfLost()
	{
		if (GlobalSettings.loseCondition)
			GlobalSettings.loseCondition = false;
    }
}