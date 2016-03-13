using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool perkChosen;

    public bool lose;
	public bool win;

    private static GameManager _instance;

    private CameraFollow _cameraFollow;
    private LevelManager _levelManager;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        _levelManager = GetComponent<LevelManager>();

        perkChosen = false;
        lose = false;
		win = false;

        if (!perkChosen)
			_cameraFollow.lockRightEdge = true;
    }

	void Update()
	{
		CheckIfLost();
		CheckIfWon();
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