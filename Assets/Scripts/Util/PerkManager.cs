using UnityEngine;

public class PerkManager : MonoBehaviour
{
	// Perk unlocked states
	public static bool axe_dtVampirism_unlocked = false;
	public static bool trinket_agressionBuddy_unlocked = false;

	// Section for variables that will determine unlocking perks
	public static int enemiesKilled = 0;

	// Perk names
	public static string axe_none_name = "Axe_None";
	public static string axe_none_desc = "Abe's Regular Axe";

	public static string axe_dtVampirism_name = "Axe_DTVampirism";
	public static string axe_dtVampirism_desc = "Perk: Vampirism\nRestores damage threshold on all heavy attacks";

	public static string trinket_agressionBuddy_name = "Trinket_AggressionBuddy";
	public static string trinket_agressionBuddy_desc = "Perk: Aggression Buddy\nRestores damage threshold with a cooldown of 30sec.";

	public static Perk activeAxePerk = null;
	public static Perk activeHatPerk = null;
	public static Perk activeTrinketPerk = null;

	public delegate void PerkEffectHandler();
	public static event PerkEffectHandler AxePerkEffect = delegate { };
	public static event PerkEffectHandler HatPerkEffect = delegate { };
	public static event PerkEffectHandler TrinketPerkEffect = delegate { };

    void Start()
    {
        GameObject[] perksInLevel = GameObject.FindGameObjectsWithTag("Perk");
        for (int i = 0; i < perksInLevel.Length; i++)
        {
            Perk p = perksInLevel[i].GetComponent<Perk>();
            p.CheckStatus();
        }

        GameObject[] axePerksInLevel = GameObject.FindGameObjectsWithTag("AbeAxe");
        for (int i = 0; i < axePerksInLevel.Length; i++)
        {
            Perk p = axePerksInLevel[i].GetComponent<Perk>();
            p.CheckStatus();
        }
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