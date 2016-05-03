using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool firstGame = true;

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
        if (PerkManager.axe_bfa_to_be_unlocked && !PerkManager.axe_bfa_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.axe_bfa_name, 1);
            Debug.Log("BFA to unlock");
        }
        if (PerkManager.axe_dtVampirism_to_be_unlocked && !PerkManager.axe_dtVampirism_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.axe_dtVampirism_name, 1);
            Debug.Log("vamp to unlock");
        }
        if (PerkManager.axe_slugger_to_be_unlocked && !PerkManager.axe_slugger_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.axe_slugger_name, 1);
            Debug.Log("slugger to unlock");
        }
        if (PerkManager.hat_bearHands_to_be_unlocked && !PerkManager.hat_bearHands_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.hat_bearHands_name, 1);
            Debug.Log("bear to unlock");
        }
        if (PerkManager.hat_stickyFingers_to_be_unlocked && !PerkManager.hat_stickyFingers_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.hat_stickyFingers_name, 1);
            Debug.Log("sticky to unlock");
        }
        if (PerkManager.trinket_agressionBuddy_to_be_unlocked && !PerkManager.trinket_agressionBuddy_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.trinket_agressionBuddy_name, 1);
            Debug.Log("buddy to unlock");
        }
        if (PerkManager.trinket_maryToddsLockette_to_be_unlocked && !PerkManager.trinket_maryToddsLockette_unlocked)
        {
            PerkManager.newPerksUnlocked = true;
            PerkManager.UpdatePerkStatus(PerkManager.trinket_maryToddsLockette_name, 1);
            Debug.Log("marytodds to unlock");
        }

        if (PerkManager.newPerksUnlocked)
            GameObject.Find("UI").GetComponent<UIManager>().NewPerks.SetActive(true);

        PreferenceManager.SetPreferences();
    }
}