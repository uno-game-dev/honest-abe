using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

	public int healAmount;
	private Health health;
	private BaseCollision collision;

	// Use this for initialization
	void Start () {
		collision = GetComponent<BaseCollision>();
		collision.OnCollision += OnCollision;
	}
	
	// Update is called once per frame
	void Update () {
		collision.Tick();
	}

	
	public void IncreaseHealth(GameObject toObject){
		if (health == null) {
			health = toObject.GetComponent<Health> ();
		}
		health.Increase (healAmount);
	}


	// Increase health when player collides with object
	private void OnCollision(RaycastHit2D hit) {
		if (hit.collider.tag == "Player") {
			IncreaseHealth (hit.transform.gameObject);
			Debug.Log ("Hit health pickup");
			DestroyImmediate (this.gameObject);
		}
	}

}
