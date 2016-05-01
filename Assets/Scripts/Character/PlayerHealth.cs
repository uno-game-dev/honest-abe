using UnityEngine;
using System;

public class PlayerHealth : Health
{
    public int damageThreshold;
    public float decreaseSecondsPerHealthPoint = 1;
	public int executionsPerformed;

	[HideInInspector]
	private HealthSlider _slider;
	private GameManager _gameManager;
	private int _tempHealth;
    private int _tempDamageThreshold;
    private float _updateSliderTime = 1;
    private Animator _animator;
    private CharacterState _characterState;

    void Start()
	{
		Initialize();
    }

    void Update()
    {
        if (health <= 0)
            Death();
		if (alive)
			// Decreases the timer to know when to update the damageSlider
			_updateSliderTime -= Time.deltaTime;
		UpdateHUD();
		if (executionsPerformed > 0)
		{
			executionsPerformed--;
			Execution();
		}
    }

    public void RefillForCutscene()
    {
        // Add 100 to Abe's health to make sure it's refilled
        Increase(100);

        // Empty the DT then add 100 to make sure it matches the health
        damageThreshold = 0;
        IncreaseDT(100);
    }

	public void Initialize()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_slider = GameObject.Find("HealthUI").GetComponent<HealthSlider>();
		health = 100;
		damageThreshold = 100;
		executionsPerformed = 0;
        alive = true;
		GetComponent<PlayerMotor>().enabled = true;
		GetComponent<PlayerControls>().enabled = true;
        _animator = GetComponent<Animator>();
        _characterState = GetComponent<CharacterState>();
    }

    public override void Increase(int amount)
    {
        _tempHealth = health;
        _tempHealth += amount;
        // If damageThreshold is equal OR health is greater than damageThreshold(i.e. currentHealh is slowly decreasing)
        if ((damageThreshold == health) || (health > damageThreshold))
        {
            // If the damageThreshold and health are both equal at 100 then do not increase anything
            if ((damageThreshold != 100) && (health != 100))
            {
                // Check to make sure CurrentHealth never goes over 100. If it will be 100 then set both to 100
                if ((_tempHealth) <= 100)
                {
                    health += amount;
                    _slider.UpdateCurrentHealth(health);
                    damageThreshold += amount;
                    _slider.UpdateDamageThreshold(damageThreshold);
                }
                else {
                    health = 100;
                    _slider.UpdateCurrentHealth(health);
                    damageThreshold = 100;
                    _slider.UpdateDamageThreshold(damageThreshold);
                }
            }

        }
        // ELSE-IF--The health starts off less than damageThreshold(2 cases)
        // The health AFTER the health increase is less than the damageThreshold 
        else if ((_tempHealth) <= damageThreshold)
        {
            // Check to make sure CurrentHealth never goes over 100
            if ((_tempHealth) <= 100)
            {
                health += amount;
                _slider.UpdateCurrentHealth(health);
            }
            else {
                health = 100;
                _slider.UpdateCurrentHealth(health);
            }
        }
        // The health AFTER the health increase is greater than damageThreshold
        else if ((_tempHealth) > damageThreshold)
        {
            // Check to make sure CurrentHealth never goes over 100. If it will be 100 then set both to 100
            if ((_tempHealth) <= 100)
            {
                health += amount;
                _slider.UpdateCurrentHealth(health);
                damageThreshold = health;
                _slider.UpdateDamageThreshold(damageThreshold);
            }
            else {
                health = 100;
                _slider.UpdateCurrentHealth(health);
                // Only bring the DT = 100 if it is less than 100. If it's above then just leave the same
                if (damageThreshold < 100)
                {
                    damageThreshold = 100;
                    _slider.UpdateDamageThreshold(damageThreshold);
                }
            }
        }
    }

    public void IncreaseDT(int amount)
    {
        _tempDamageThreshold = damageThreshold;
        _tempDamageThreshold += amount;

        if (_tempDamageThreshold <= 120)
        {
            damageThreshold += amount;
            _slider.UpdateDamageThreshold(damageThreshold);
        }
        else {
            damageThreshold = 120;
            _slider.UpdateDamageThreshold(damageThreshold);
        }
    }

    public override void Decrease(int damage)
    {
        if ((PerkManager.activeTrinketPerk != null) && (Perk.performMaryToddsTimeStamp >= Time.time)) {
			Debug.Log ("Mary Todd's Lockette is Actived");
			return;
		} else {
			Debug.Log (Perk.maryToddsLocketteIsActive);
			// Temp variable for damageThreshold
			_tempDamageThreshold = damageThreshold - damage;
			if (_tempDamageThreshold <= 0) {
				health -= Math.Abs (_tempDamageThreshold);
				_slider.UpdateCurrentHealth (health);
				damageThreshold = 0;
			} else {
				damageThreshold -= damage;
			}
			_slider.UpdateDamageThreshold (damageThreshold);
		}
    }

    // Updates the health and damageThreshold 
    private void UpdateHUD()
    {
        // CurrentHealth is slowly decreasing to eventually equal damageThreshold 
        if (health > damageThreshold)
        {
            // Time to update currentHealthSlider
            if (_updateSliderTime < 0)
            {
                // Decrease the health in increments of 5
                health -= 1;
                // If the player has lost all it's health
                // Update currentHeathSlider
                _slider.UpdateCurrentHealth(health);
                // Reset the timer for updating the HealthSlider
                _updateSliderTime = decreaseSecondsPerHealthPoint;
            }
        }
    }
	
    private void Execution()
    {
        // Make sure damageThreshold does not go above 120
        if (damageThreshold <= 110)
        {
            SoundPlayer.Play("DT Restoration");
            // Abe performed an execution increase damageThreshold by 10
            damageThreshold += 10;
        }
        else
            // Abe performed an execution and damageThreshold is 110 or greater adjust damageThreshold to 120
            damageThreshold = 120;
        // Update damageThreshold
        _slider.UpdateDamageThreshold(damageThreshold);
    }

    private void Death()
	{
        if (!alive)
            return;

		alive = false;
		EventHandler.SendEvent(EventHandler.Events.GAME_LOSE);
        SoundPlayer.Play("Abe Death");
        _animator.TransitionPlay("Abe Death");
        _characterState.SetState(CharacterState.State.Dead);

		GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = false;
		GetComponent<BaseCollision>().enabled = false;
	}
}
