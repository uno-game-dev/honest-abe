using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthSlider : MonoBehaviour {

	public Slider bossHealth;

	//Different levels of the boss's health
	public Slider _75PercentHealth;
	public Slider _50PercentHealth;
	public Slider _25PercentHealth;


	// Use this for initialization
	void Start () {
		//Provides a visual aid for the player to see the percentage of the boss's health
		_75PercentHealth.value = 75;
		_50PercentHealth.value = 50;
		_25PercentHealth.value = 25;
	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateBossHealth(int newValue){
		bossHealth.value = newValue;
	}

	public void Reset(int startHealth){
		bossHealth.maxValue = startHealth;
		bossHealth.value = startHealth;
	}
}
