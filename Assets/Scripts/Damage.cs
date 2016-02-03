﻿using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int damageAmount;
	public float damageRate = 1f;
	private Health health;
	private BaseCollision collision;

	// Use this for initialization
	void Start () {
		collision = GetComponent<BaseCollision>();
		collision.OnCollision += OnCollision;
	}

	// Update is called once per frame
	void Update () {
		// collision.Move(Vector2.zero);
	}

	public void ExecuteDamage(GameObject toObject){
		if (health == null) {
			health = toObject.GetComponent<Health> ();
		}
		health.Decrease (damageAmount, damageRate);

	}

	private void OnCollision(RaycastHit2D hit) {
		if (hit.collider.tag == "Enemy") {
			ExecuteDamage(hit.transform.gameObject);
			Debug.Log ("Hit enemy");
		}
		if (hit.collider.tag == "Player") {
			ExecuteDamage(hit.transform.gameObject);
			Debug.Log ("Hit player");
		}
	}



}
