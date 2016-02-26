using UnityEngine;
using System.Collections.Generic;

public class PerkManager : MonoBehaviour
{

    [HideInInspector]
    public List<Perk> perkList = new List<Perk>();
    
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
            perkList.Add(p);
        }

        GameObject[] axePerksInLevel = GameObject.FindGameObjectsWithTag("AbeAxe");
        for (int i = 0; i < axePerksInLevel.Length; i++)
        {
            Perk p = axePerksInLevel[i].GetComponent<Perk>();
            p.CheckStatus();
            perkList.Add(p);
        }
    }

    public static void PerformPerkEffects()
    {
        Debug.Log("Perk Effects Performed");
        if (activeAxePerk != null) AxePerkEffect();
        if (activeHatPerk != null) HatPerkEffect();
        if (activeTrinketPerk != null) TrinketPerkEffect();
    }

    public static void UpdatePerkStatus(string perk, int status) {
        PlayerPrefs.SetInt(perk, status);
    }
}