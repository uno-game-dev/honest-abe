using UnityEngine;

public class PreferenceManager : MonoBehaviour
{

    void Awake()
    {
        // Axe DT Vampirism
        if (!PlayerPrefs.HasKey(GlobalSettings.axe_dtVampirism_name))
            PlayerPrefs.SetInt(GlobalSettings.axe_dtVampirism_name, 0);

        GlobalSettings.axe_dtVampirism_unlocked = PlayerPrefs.GetInt(GlobalSettings.axe_dtVampirism_name) == 1;
    }

}