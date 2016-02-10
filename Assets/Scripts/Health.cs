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
		if (gameObject.tag == "Player") {
			Debug.Log ("Player health is: " + health);
		}
		if (gameObject.tag == "Enemy") {
			Debug.Log ("Enemy health is: " + health);
		}
	}

}
