using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerkManager : MonoBehaviour {

    public List<Perk> perkList = new List<Perk>();

    void Start () {
        PreferenceManager.UpdatePerkStatus();

        GameObject[] perksInLevel = GameObject.FindGameObjectsWithTag("Perk");
        for (int i = 0; i < perksInLevel.Length; i++)
        {
            perkList.Add(perksInLevel[i].GetComponent<Perk>());
        }
	}
	


}
