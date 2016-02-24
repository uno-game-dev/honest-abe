using UnityEngine;

public class PreferenceManager : MonoBehaviour {

	void Awake () {

        // Fire Axe
        if(!PlayerPrefs.HasKey(GlobalSettings.axe_fire_name))
            PlayerPrefs.SetInt(GlobalSettings.axe_fire_name, 0);

        // DT Vampirism Hat
        //if (!PlayerPrefs.HasKey(GlobalSettings.hat_dtVampirism_name))
            PlayerPrefs.SetInt(GlobalSettings.hat_dtVampirism_name, 1);

        GlobalSettings.axe_fire_unlocked = PlayerPrefs.GetInt(GlobalSettings.axe_fire_name) == 1;
        GlobalSettings.hat_dtVampirism_unlocked = PlayerPrefs.GetInt(GlobalSettings.hat_dtVampirism_name) == 1;
	}

    public static void UpdatePerkStatus(string perk, int status) {
        PlayerPrefs.SetInt(perk, status);
    }

}
