using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Health {

    private int damageThreshold;
    public int DamageThreshold
    {
        get
        {
            return damageThreshold;
        }
        set
        {
            damageThreshold += (value - damageThreshold);
            if (damageThreshold > 120)
                damageThreshold = 120;
        }
    }

	[HideInInspector]
	public int currentHealth;
	private float nextHitToPlayer;
	private float updateHealthSliderTime = 2;
	private bool isDead;
	[HideInInspector]
	public bool executionPerformed = false;
	private HealthSlider healthSlider;

	// Use this for initialization
	void Start () {
		damageThreshold = 100;
		currentHealth = 100;
		healthSlider = GetComponent<HealthSlider>();
		isDead = false;
	}

	// Update is called once per frame
	void Update () {
		/**
		//TESTING PURPOSES
		//Test execution
		if (Input.GetKeyDown(KeyCode.E)) {
			Debug.Log ("Performed EXECUTION!!!");
			executionPerformed = true;
		}	
		//Test healthpickup
		if (Input.GetKeyDown(KeyCode.H)) {
			Debug.Log ("HEALTH PICKUP!!!");
			Increase (10);
		}	
		if (Input.GetKeyDown(KeyCode.J)) {
			Debug.Log ("HEALTH PICKUP!!!");
			Increase (5);
		}**/

		if(!isDead){
			//Decreases the timer to know when to update the damageSlider
			updateHealthSliderTime -= Time.deltaTime;
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
				damageThreshold = 100;
				healthSlider.UpdateDamageThreshold(damageThreshold);
			}
		}
	}

	public override void Decrease(int damage, float damageRate){
		if (Time.time > nextHitToPlayer) {
			nextHitToPlayer = Time.time + damageRate;
			damageThreshold -= damage;
			healthSlider.UpdateDamageThreshold(damageThreshold);
			updateHealthSliderTime = 1.5f;
		}
	}

	//Updates the currentHealth and damageThreshold 
	void UpdateHUD(){
		//CurrentHealth is slowly decreasing to eventually equal damageThreshold 
		executionPerformed = GlobalSettings.executionPerformed;
        if (currentHealth > damageThreshold) {
			//Time to update currentHealthSlider
			if (updateHealthSliderTime < 0) {
				if (!executionPerformed) {
					//Decrease the currentHealth in increments of 5
					currentHealth -= 1;
					// If the player has lost all it's health
					if (currentHealth <= 0) {
						//Abe is dead :(
						Death ();
					}	
					//Update currentHeathSlider
					healthSlider.UpdateCurrentHealth(currentHealth);
					//Reset the timer for updating the HealthSlider
					updateHealthSliderTime = 1.5f;
				}else {
					//Else -- An execution has been performed
					//Make sure damageThreshold does not go above 120
					if (damageThreshold <= 110) {
						//Abe performed an execution increase damageThreshold by 10
						damageThreshold += 10;
					}
					else{
						//Abe performed an execution and damageThreshold is 110 or greater adjust damageThreshold to 120
						damageThreshold = 120;
					}
					//Update damageThreshold
					healthSlider.UpdateDamageThreshold(damageThreshold);

					//Reset execution check
					executionPerformed = false;
				}
			}
		}else {
			//Else -- Abe did not get hit but performed an execution
			//The damageThreshold is above or equal to currentHealth and Abe performed execution
			if (executionPerformed) {
				//Make sure damageThreshold does not go above 120
				if (damageThreshold <= 110) {
					//Abe performed an execution increase damageThreshold by 10
					damageThreshold += 10;
				}
				else{
					//Abe performed an execution and damageThreshold is 110 or greater adjust damageThreshold to 120
					damageThreshold = 120;
				}
				//Update damageThreshold
				healthSlider.UpdateDamageThreshold(damageThreshold);

				//Reset execution check
				executionPerformed = false;
			}
		}
	}

	void Death(){
		isDead = true;
		Debug.Log ("Abe is dead :( ");
		//Disable PlayerMotor script 
		gameObject.GetComponent<PlayerMotor> ().enabled = !enabled;
		//Set animation for dead player
		//Turn off any attack effects
		//Alert the player that they died. Ask the player to play again
	}
}