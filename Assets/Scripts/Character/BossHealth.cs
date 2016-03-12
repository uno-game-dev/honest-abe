using UnityEngine;
using System;

public class BossHealth : Health
{	
	private int tempHealth;
	private BossHealthSlider bossHealthSlider;
	[HideInInspector]
	public bool isDead;
	private GameManager _gameManager;

	// Use this for initialization
	void Start () {
		isDead = false;
		bossHealthSlider = GameObject.Find ("BossHealthUI").GetComponent<BossHealthSlider> ();
		bossHealthSlider.Reset(health);
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
	}

	public void Dead(){
		isDead = true;
		//Need to check if the FINAL BOSS just died once more bosses are implemented
		/**
		if(gameObject.tag == "Boss"){ // Testing if statement -- need to erase
			_gameManager.win = true; // TESTING for Win game in alpha
		}**/

		//Need to check which boss was just destroyed to transition to the next scene
		Destroy(gameObject);
		UIManager._bossHealthUI.enabled = false;
	}
}