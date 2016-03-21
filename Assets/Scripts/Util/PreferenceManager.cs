using UnityEngine;

public class PreferenceManager : MonoBehaviour
{

	void Awake()
	{
		// Axe DT Vampirism
		if (!PlayerPrefs.HasKey(PerkManager.axe_dtVampirism_name))
			PlayerPrefs.SetInt(PerkManager.axe_dtVampirism_name, 0);

		PerkManager.axe_dtVampirism_unlocked = PlayerPrefs.GetInt(PerkManager.axe_dtVampirism_name) == 1;

		// Trinket Aggression Buddy 
		if (!PlayerPrefs.HasKey(PerkManager.trinket_agressionBuddy_name))
			PlayerPrefs.SetInt(PerkManager.trinket_agressionBuddy_name, 0);

		PerkManager.trinket_agressionBuddy_unlocked = PlayerPrefs.GetInt(PerkManager.trinket_agressionBuddy_name) == 1;
	}

}