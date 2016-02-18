using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	//TESTING
	public Text enemyHealthText;

    private PerkManager perkManager;

    void Start()
    {
        perkManager = GameObject.Find("PerkManager").GetComponent<PerkManager>();
    }

    void Update()
    {

    }

}
