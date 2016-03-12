using UnityEngine;
using System;

public class BossHealth : Health
{	
	private int tempHealth;
	private BossHealthSlider bossHealthSlider;
	[HideInInspector]
	public bool isDead;

	// Use this for initialization
	void Start () {
		isDead = false;
		bossHealthSlider = GameObject.Find ("BossHealthUI").GetComponent<BossHealthSlider> ();
		bossHealthSlider.Reset(health);
		Debug.Log ("Did we find the bossUI: " + bossHealthSlider);
	}

	// Update is called once per frame
	void Update () {
	}

	public override void Increase(int amount){
		tempHealth = health;
		tempHealth += amount;

		if (tempHealth >= 100) {
			health = 100;
		} else {
			health += amount;
		}

		bossHealthSlider.UpdateBossHealth (health);
		Debug.Log ("The bear's health is: " + health);
	}

	public override void Decrease(int amount){
		tempHealth = health;
		tempHealth -= amount;

		if (tempHealth <= 0) {
			health = 0;
			Dead();
		} else {
			health -= amount;
		}

		bossHealthSlider.UpdateBossHealth(health);
		Debug.Log ("The bear's health is: " + health);
	}

	public void Dead(){
		isDead = true;
		//Need to check if the FINAL BOSS just died once more bosses are implemented
		GlobalSettings.bossFight = false;
		if(gameObject.tag == "Boss"){ // Testing if statement -- need to erase
			GameManager.win = true; // TESTING for Win game in alpha
		}
		//Need to check which boss was just destroyed to transition to the next scene
		Destroy(gameObject);
		//UIManager._bossHealthUI.SetActive(false);
		UIManager._bossHealthUI.enabled = false;
	}

}