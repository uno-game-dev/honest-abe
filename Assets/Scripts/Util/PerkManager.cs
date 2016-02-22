using UnityEngine;
using System.Collections.Generic;

public class PerkManager : MonoBehaviour {

    [HideInInspector] public List<Perk> perkList = new List<Perk>();

    public Perk activeAxePerk = null;
    public Perk activeHatPerk = null;
    public Perk activeTrinketPerk = null;

    void Start () {
        GameObject[] perksInLevel = GameObject.FindGameObjectsWithTag("Perk");
        for (int i = 0; i < perksInLevel.Length; i++)
        {
            Perk p = perksInLevel[i].GetComponent<Perk>();
            p.CheckStatus();
            perkList.Add(p);
        }
	}
	
}
