using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PerkManager : MonoBehaviour
{

    /*
     * Individual Perk Information
     */
    // Perk unlocked states
    public static bool axe_dtVampirism_unlocked = false;
    public static bool axe_bfa_unlocked = false;
    public static bool axe_slugger_unlocked = false;
    public static bool hat_bearHands_unlocked = false;
	public static bool hat_stickyFingers_unlocked = false;
	public static bool trinket_agressionBuddy_unlocked = false;
	public static bool trinket_maryToddsLockette_unlocked = false;

    // Perk names
    public static string axe_none_name = "Axe_None";
    public static string axe_none_desc = "Abe's Regular Axe";
    public static string axe_none_lock_desc = "How are you even seeing this?";

    public static string hat_none_name = "Hat_None";
    public static string hat_none_desc = "Abe's Regular Hat";
    public static string hat_none_lock_desc = "How are you even seeing this?";

    public static string axe_dtVampirism_name = "Axe_DTVampirism";
    public static string axe_dtVampirism_desc = "Perk: Vampirism\nRestores damage threshold on all heavy attacks";
    public static string axe_dtVampirism_lock_desc = "Perk: Vampirism\nThis Perk is Locked!";

    public static string axe_bfa_name = "Axe_BFA";
    public static string axe_bfa_desc = "Perk: B.F.A.\nA bigger, stronger axe for your pleasure.";
    public static string axe_bfa_lock_desc = "Perk: B.F.A\nThis Perk is Locked!";

    public static string axe_slugger_name = "Axe_Slugger";
    public static string axe_slugger_desc = "Perk: Slugger\nStronger combos and harder knockbacks";
    public static string axe_slugger_lock_desc = "Perk: Slugger\nThis Perk is Locked!";

    public static string hat_bearHands_name = "Hat_bearHands";
    public static string hat_bearHands_desc = "Perk: Bear Hands\nIncreased damage on all empty-handed attacks";
    public static string hat_bearHands_lock_desc = "Perk: Bear Hands\nThis Perk is Locked!";

	public static string trinket_agressionBuddy_name = "Trinket_AggressionBuddy";
	public static string trinket_agressionBuddy_desc = "Perk: Aggression Buddy\nRestores damage threshold with a cooldown of 30sec.";
    public static string trinket_agressionBuddy_lock_desc = "Perk: Aggression Buddy\nThis Perk is Locked!";

	public static string trinket_maryToddsLockette_name = "Trinket_MaryToddsLockette";
	public static string trinket_maryToddsLockette_desc = "Perk: Mary Todd's Locket\nProvides invincibility with a cooldown of 120sec.";
    public static string trinket_maryToddsLockette_lock_desc = "Perk: Mary Todd's Locket\nThis Perk is Locked!";

	public static string hat_stickyFingers_name = "Hat_StickyFingers";
	public static string hat_stickyFingers_desc = "Perk: Sticky Fingers\nEnables the ablity to steal weapons";
    public static string hat_stickyFingers_lock_desc = "Perk: Sticky Fingers\nThis Perk is Locked!";

    /*
     * Individual Perk Unlock Requirements
     */
    public static int enemiesKilled = 0;

    /*
     * Core PerkManager Componenets
     */

	public static Perk activeAxePerk = null;
	public static Perk activeHatPerk = null;
	public static Perk activeTrinketPerk = null;

	public static event PerkEffectHandler AxePerkEffect = delegate { };
	public static event PerkEffectHandler HatPerkEffect = delegate { };
	public static event PerkEffectHandler TrinketPerkEffect = delegate { };

    public delegate void PerkEffectHandler();

    /*
     * List of all perks in the game
     */
    public static List<Perk> perkList;

    /*
	 * Perk Pick Up Scene
	 */
    public static bool hatPerkChosen = false;
    public static bool trinketPerkChosen = false;
    public static bool axePerkChosen = false;

    private static CameraFollow cameraFollow;

	private WorldGenerator worldGen;
	private LevelManager levelManager;

	/*
	 * Trinket UI
	 */
	public static int trinketTime = 100;
	public static int maryToddsTrinketTime = 100;
    public static float decreaseTrinketBarRate = 0;
    public static float decreaseMaryToddsBarRate = 0;
    public static bool updateTrinketBar = false;
    public static bool updateMaryToddsBar = false;

    private static TrinketSlider trinketSlider;

	private float nextTrinketDecrease = 0f;
	private float nextMaryToddsDecrease = 0f;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == GlobalSettings.levelOneSceneName)
            perkList = new List<Perk>();

        GameObject[] perksInLevel = GameObject.FindGameObjectsWithTag("Perk");
        for (int i = 0; i < perksInLevel.Length; i++)
        {
            Perk p = perksInLevel[i].GetComponent<Perk>();
            p.CheckStatus();
            perkList.Add(p);
        }

        GameObject[] axePerksInLevel = GameObject.FindGameObjectsWithTag("AbeAxe");
        for (int i = 0; i < axePerksInLevel.Length; i++)
        {
            Perk p = axePerksInLevel[i].GetComponent<Perk>();
            p.CheckStatus();
            perkList.Add(p);
        }

		cameraFollow = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
		worldGen = GameObject.Find ("Level").GetComponent<WorldGenerator> ();
		levelManager = GameObject.Find ("GameManager").GetComponent<LevelManager> ();
		trinketSlider = GameObject.Find ("TrinketUI").GetComponent<TrinketSlider> ();
    }

	void Update()
    {
        if (levelManager.currentScene == 0 && (!hatPerkChosen || !axePerkChosen))
            cameraFollow.lockRightEdge = true;
        else
            cameraFollow.lockRightEdge = false;

        if (updateTrinketBar && (Time.time > nextTrinketDecrease)){
			nextTrinketDecrease = Time.time + decreaseTrinketBarRate;
			trinketTime -= 1;
			trinketSlider.UpdateTrinket (trinketTime);
		}
		if(updateMaryToddsBar && (Time.time > nextMaryToddsDecrease )){
			nextMaryToddsDecrease = Time.time + decreaseMaryToddsBarRate;
			maryToddsTrinketTime -= 1;
			trinketSlider.UpdateMaryToddsTrinket(maryToddsTrinketTime);
		}
	}

    public void Reset()
    {
        hatPerkChosen = false;
        trinketPerkChosen = false;
        axePerkChosen = false;
    }

    public static void PerformPerkEffects(Perk.PerkCategory type)
    {
        if (type == Perk.PerkCategory.AXE)
        {
            if (activeAxePerk != null) AxePerkEffect();
        }
        else if (type == Perk.PerkCategory.HAT)
        {
            if (activeHatPerk != null) HatPerkEffect();
        }
        else if (type == Perk.PerkCategory.TRINKET)
        {
            if (activeTrinketPerk != null) TrinketPerkEffect();
        }
    }

    public static void UpdatePerkStatus(string perk, int status)
    {
        PlayerPrefs.SetInt(perk, status);
    }
}