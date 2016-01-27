using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text healthText;
	public int health;
	public int damage;

	// Use this for initialization
	void Start () {
		health = 100;
		UpdateHealth ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void UpdateHealth(){
		healthText.text = "Health: " + health;
	}

	public void DecreaseHealth(){
		health -= damage;
		UpdateHealth ();
	}
}
