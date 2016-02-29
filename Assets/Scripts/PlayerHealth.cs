using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Health {
	public int damageThreshold = 100;
	public int currentHealth = 100;
    public float decreaseSecondsPerHealthPoint = 1;
	[HideInInspector]
	private float updateHealthSliderTimer = 1;
	private bool isDead;
	private HealthSlider healthSlider;

	// Use this for initialization
	void Start ()
    {
		GlobalSettings.executionsPerformed = 0;
		healthSlider = GetComponent<HealthSlider>();
		isDead = false;
	}

	// Update is called once per frame
	void Update ()
    {
		if(!isDead){
			//Decreases the timer to know when to update the damageSlider
			updateHealthSliderTimer -= Time.deltaTime;
			//Debug.Log ("Time remaining for updating Damage: " + updateDamageSliderTime);
		}
		UpdateHUD ();
	}

	public override void Increase(int amount){
		//Temp variable for currentHealth
		int tempCurrentHealth = currentHealth;
		tempCurrentHealth += amount;

		//If damageThreshold is equal OR currentHealth is greater than damageThreshold(i.e. currentHealh is slowly decreasing)
		if((damageThreshold == currentHealth) || (currentHealth > damageThreshold)){
			//If the damageThreshold and currentHealth are both equal at 100 then do not increase anything
			if ((damageThreshold != 100) && (currentHealth != 100)) {
				//Check to make sure CurrentHealth never goes over 100. If it will be 100 then set both to 100
				if ((tempCurrentHealth) <= 100) {
					currentHealth += amount;
					healthSlider.UpdateCurrentHealth(currentHealth);
					damageThreshold += amount;
					healthSlider.UpdateDamageThreshold (damageThreshold);
				}   else {
					currentHealth = 100;
					healthSlider.UpdateCurrentHealth(currentHealth);
					damageThreshold = 100;
					healthSlider.UpdateDamageThreshold (damageThreshold);
				}
			}
		}
		//ELSE-IF--The currentHealth starts off less than damageThreshold(2 cases)
		//The currentHealth AFTER the health increase is less than the damageThreshold 
		else if((tempCurrentHealth) <= damageThreshold){
			//Check to make sure CurrentHealth never goes over 100
			if ((tempCurrentHealth) <= 100) {
				currentHealth += amount;
				healthSlider.UpdateCurrentHealth(currentHealth);
			}   else {
				currentHealth = 100;
				healthSlider.UpdateCurrentHealth(currentHealth);
			}
		}
		//The currentHealth AFTER the health increase is greater than damageThreshold
		else if((tempCurrentHealth) > damageThreshold){
			//Check to make sure CurrentHealth never goes over 100. If it will be 100 then set both to 100
			if ((tempCurrentHealth) <= 100) {
				currentHealth += amount;
				healthSlider.UpdateCurrentHealth(currentHealth);
				damageThreshold = currentHealth;
				healthSlider.UpdateDamageThreshold(damageThreshold);
			}   else {
				currentHealth = 100;
				healthSlider.UpdateCurrentHealth(currentHealth);
				//Only bring the DT = 100 if it is less than 100. If it's above then just leave the same
				if (damageThreshold < 100) {
					damageThreshold = 100;
					healthSlider.UpdateDamageThreshold (damageThreshold);
				}
			}
		}
	}

	public void IncreaseDT(int amount){
		//Temp variable for damageThreshold
		int tempDamageThreshold = damageThreshold;
		tempDamageThreshold += amount;

		if (tempDamageThreshold <= 120) {
			damageThreshold += amount;
			healthSlider.UpdateDamageThreshold (damageThreshold);
		} else {
			damageThreshold = 120;
			healthSlider.UpdateDamageThreshold (damageThreshold);
		}
	} 

	public override void Decrease(int damage){
		//Temp variable for damageThreshold
		int tempDamageThreshold = damageThreshold - damage;
		if (tempDamageThreshold <= 0) {
			damageThreshold = 0;
		} else {
			damageThreshold -= damage;
		}
		healthSlider.UpdateDamageThreshold(damageThreshold);
	}

	//Updates the currentHealth and damageThreshold 
	void UpdateHUD(){
		//CurrentHealth is slowly decreasing to eventually equal damageThreshold 
        if (currentHealth > damageThreshold) {
			//Time to update currentHealthSlider
			if (updateHealthSliderTimer < 0) {
			    //Decrease the currentHealth in increments of 5
			    currentHealth -= 1;
			    // If the player has lost all it's health
			    if (currentHealth <= 0) {
				    //Abe is dead :(
				    Death();
			    }	
			    //Update currentHeathSlider
			    healthSlider.UpdateCurrentHealth(currentHealth);
			    //Reset the timer for updating the HealthSlider
			    updateHealthSliderTimer = decreaseSecondsPerHealthPoint;
			}
		}
        if (GlobalSettings.executionsPerformed > 0)
        {
            GlobalSettings.executionsPerformed--;
            Execution();
        }

	}

    void Execution()
    {
        //Make sure damageThreshold does not go above 120
        if (damageThreshold <= 110)
        {
            //Abe performed an execution increase damageThreshold by 10
            damageThreshold += 10;
        }
        else {
            //Abe performed an execution and damageThreshold is 110 or greater adjust damageThreshold to 120
            damageThreshold = 120;
        }
        //Update damageThreshold
        healthSlider.UpdateDamageThreshold(damageThreshold);
    }

    void Death(){
		isDead = true;
		GameManager.lost = true;
		Debug.Log ("Abe is dead :( ");
		//Disable PlayerMotor script 
		gameObject.GetComponent<PlayerMotor> ().enabled = !enabled;
		//Turn off any attack effects
		gameObject.GetComponent<PlayerControls> ().enabled = !enabled;
		//Set animation for dead player
	}
}
