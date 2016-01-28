using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int damageAmount;
	public float damageRate = 1f;
	private Health health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DoDamage(GameObject toObject){
		health = toObject.GetComponent<Health>();
		health.Decrease (damageAmount, damageRate);
	}
}
