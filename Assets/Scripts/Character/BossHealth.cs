using UnityEngine;
using System;

public class BossHealth : Health
{
	private int tempHealth;
	private BossHealthSlider bossHealthSlider;
	[HideInInspector]
	private GameManager _gameManager;

	// Use this for initialization
	void Start()
	{
		alive = true;
		bossHealthSlider = GameObject.Find("BossHealthUI").GetComponent<BossHealthSlider>();
		bossHealthSlider.Reset(health);
	}

	// Update is called once per frame
	void Update()
	{
	}

	public override void Increase(int amount)
	{
		tempHealth = health;
		tempHealth += amount;

		if (tempHealth >= 100)
			health = 100;
		else
			health += amount;

		bossHealthSlider.UpdateBossHealth(health);
	}

	public override void Decrease(int amount)
	{
		tempHealth = health;
		tempHealth -= amount;

		if (tempHealth <= 0)
		{
			health = 0;
			alive = false;
			Destroy(gameObject);
		}
		else
			health -= amount;

		bossHealthSlider.UpdateBossHealth(health);
	}
}