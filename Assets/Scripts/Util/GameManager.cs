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
        Debug.Log("Perks Unlocked");

        // This is super ugly, but I had to do what immediately came to mind
        // and don't have time to make it nicer
        if (PerkManager.axe_bfa_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.axe_bfa_name, 1);
        }
        if (PerkManager.axe_dtVampirism_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.axe_dtVampirism_name, 1);
        }
        if (PerkManager.axe_slugger_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.axe_slugger_name, 1);
        }
        if (PerkManager.hat_bearHands_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.hat_bearHands_name, 1);
        }
        if (PerkManager.hat_stickyFingers_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.hat_stickyFingers_name, 1);
        }
        if (PerkManager.trinket_agressionBuddy_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.trinket_agressionBuddy_name, 1);
        }
        if (PerkManager.trinket_maryToddsLockette_to_be_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.trinket_maryToddsLockette_name, 1);
        }

        if (PerkManager.newPerksUnlocked)
            GameObject.Find("UI").GetComponent<UIManager>().NewPerks.SetActive(true);

        PreferenceManager.SetPreferences();
    }
}