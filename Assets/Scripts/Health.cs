using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health;
	public GameManager gm;
	private float nextHit;

	// Use this for initialization
	void Start () {
		health = 100;
		UpdateHealth ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Increase(int amount){
		health += amount;
	}

	public void Decrease(int damage, float damageRate){
		if (Time.time > nextHit) {
			nextHit = Time.time + damageRate;
			health -= damage;
			UpdateHealth ();
		}
	}

	void UpdateHealth(){
		//Check and see if the object is Player just in case we display eniemes health we can differentiate
		if (gameObject.tag == "Player") {
			gm.healthText.text = "Health: " + health;
		}
	}

}
