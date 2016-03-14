using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool perkChosen;

    public bool lose;
	public bool win;

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
        lose = false;
        win = false;
        GlobalSettings.currentSceneIsNew = false;
    }

	public void CheckIfWon()
	{
		//Checks if the boss health is 0 -- for alpha
		if (win)
		{
			win = false;
			PerkManager.UpdatePerkStatus(GlobalSettings.axe_dtVampirism_name, 1);
            _levelManager.loadNextLevel();
		}
	}

	public void CheckIfLost()
	{
		if (lose)
			lose = false;
    }
}