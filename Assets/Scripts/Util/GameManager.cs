using UnityEngine;

public class GameManager : MonoBehaviour
{
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
		PerkManager.UpdatePerkStatus(PerkManager.axe_dtVampirism_name, 1);
		foreach (Perk p in PerkManager.perkList)
        {
            if (p.setToBeUnlocked)
                PerkManager.UpdatePerkStatus(p.perkName, 1);
        }
        _levelManager.currentScene++;
	}
}