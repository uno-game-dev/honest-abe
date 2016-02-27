using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int health;
	private float nextHit;

	// Use this for initialization
	void Start () {
		UpdateHealth ();
	}

	// Update is called once per frame
	void Update () {

	}

	public virtual void Increase(int amount){
		health += amount;
        UpdateHealth();
	}

	public virtual void Decrease(int damage, float damageRate){
		int tempHealth = health;
		if (Time.time > nextHit) {
			nextHit = Time.time + damageRate;
            
            //If the hit would kill the gameObject
            if ((tempHealth -= damage) <= 0) {
				health = 0;
                // Execution Check
                if (gameObject.tag != "Player" && GlobalSettings.performingHeavyAttack)
                {
                    GlobalSettings.executionPerformed = true;
                    ShowExecution();
                }
                if (gameObject.tag == "Boss"){
					GlobalSettings.bossFight = false;
					GameManager.BossFight ();
					GameManager.win = true; // TESTING for Win game in alpha
                }
                UpdateHealth ();
				Destroy (gameObject);
			} else {
				health -= damage;
				UpdateHealth ();
            }
		}
	}
	//For testing purposes
	public virtual void UpdateHealth(){
		if (gameObject.tag == "Enemy") {
			//Debug.Log ("Enemy health is: " + health);
		}
    }

    public void ShowExecution()
    {
        GameObject number = new GameObject();
        number.name = "Execution";
        TextMesh tm = number.AddComponent<TextMesh>();
        tm.text = "R.I.P.";
        tm.fontSize = 24;
        tm.color = Color.red;
        tm.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        tm.transform.position = transform.position;
        FloatUpAndDestroy f = number.AddComponent<FloatUpAndDestroy>();
        f.floatGravityMultiplier = 0.5f;
        f.floatVelocity = 2;
    }

}
