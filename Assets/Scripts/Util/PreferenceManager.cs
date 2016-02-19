using UnityEngine;

public class PreferenceManager : MonoBehaviour {

	void Awake () {

        if(!PlayerPrefs.HasKey("Axe_Fire"))
            PlayerPrefs.SetInt("Axe_Fire", 0);

        if (!PlayerPrefs.HasKey("Hat_DTVampirism"))
            PlayerPrefs.SetInt("Hat_DTVampirism", 0);

        GlobalSettings.axe_fire_unlocked = PlayerPrefs.GetInt("Axe_Fire") == 1;
        GlobalSettings.hat_dtVampirism_unlocked = PlayerPrefs.GetInt("Hat_DTVampirism") == 1;
	}

    public void UpdatePerkStatus(string perk, int status) {
        PlayerPrefs.SetInt(perk, status);
    }

}
