using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int damageAmount;
	public float damageRate = 1f;
	private Health health;
	private PlayerHealth playerHealth;
	private BaseCollision collision;

	void Start () {
		collision = GetComponent<BaseCollision>();
		collision.OnCollision += OnCollision;
	}

	void Update () {
        collision.Tick();
	}

	public void ExecuteDamage(GameObject toObject){
		if (toObject.tag == "Enemy") {
			if (health == null) {
				health = toObject.GetComponent<Health> ();
			}
			health.Decrease (damageAmount, damageRate);
		}
		if (toObject.tag == "Player") {
			if (playerHealth == null) {
				playerHealth = toObject.GetComponent<PlayerHealth> ();
			}
			playerHealth.Decrease (damageAmount, damageRate);
		}
	}

	private void OnCollision(RaycastHit2D hit) {
		if ((hit.collider.tag == "Enemy") || (hit.collider.tag == "Player") ) {
			ExecuteDamage(hit.transform.gameObject);
		}
	}
}
