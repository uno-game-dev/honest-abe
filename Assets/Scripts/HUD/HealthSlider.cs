using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {
	public Slider damageThresholdSlider;
	public Slider currentHealthSlider;
	public Slider amountOfDTSlider;

	// Use this for initialization
	void Start () {
		damageThresholdSlider.value = 100;
		currentHealthSlider.value = 100;
	}
	
	// Update is called once per frame
	void Update () {
		//Check to see if damageThreshold is greater than 100 to set maxDamageThreshold
		if (damageThresholdSlider.value > 100) {
			//Change the color of damageThreshold to Green because it > 100
			amountOfDTSlider.transform.Find ("Fill Area").gameObject.GetComponentInChildren<Image> ().color = Color.green;
		} else if (damageThresholdSlider.value > currentHealthSlider.value) {
			amountOfDTSlider.transform.Find ("Fill Area").gameObject.GetComponentInChildren<Image> ().color = Color.yellow;
		} else {
			amountOfDTSlider.transform.Find ("Fill Area").gameObject.GetComponentInChildren<Image> ().color = Color.black;
		}
	}

	public void UpdateDamageThreshold(int newValue){
		damageThresholdSlider.value = newValue;
		amountOfDTSlider.value = newValue;
	}

	public void UpdateCurrentHealth(int newValue){
		currentHealthSlider.value = newValue;
	}
}
