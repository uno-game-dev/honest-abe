using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerkManager : MonoBehaviour {

    public List<Perk> perkList = new List<Perk>();

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
