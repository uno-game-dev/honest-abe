using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool perkChosen;

    public bool lose;
	public bool win;

    public bool sceneIsNew;

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

        if (sceneIsNew)
            InitializeLevel();
    }

	void Update()
    {
        if (_levelManager.currentSceneIsNew)
            InitializeLevel();
        if (!perkChosen)
            _cameraFollow.lockRightEdge = true;
        CheckIfLost();
		CheckIfWon();
	}

    // Runs when a scene is loaded
    public void InitializeLevel()
    {
        _cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        perkChosen = false;
        lose = false;
        win = false;
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