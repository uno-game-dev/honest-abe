using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	[HideInInspector]
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
			Debug.Log ("Enemy health is: " + health);
		}
	}

}
