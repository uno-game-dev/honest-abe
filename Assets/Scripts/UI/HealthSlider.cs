using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
	public Slider damageThresholdSlider;
	public Slider currentHealthSlider;
	public Slider amountOfDTSlider;
	public Slider nonDecreasingHealthSlider; //visual to see the difference btwn the decreasing health

	// Use this for initialization
	void Start()
	{
		damageThresholdSlider.value = 100;
		currentHealthSlider.value = 100;
	}

	// Update is called once per frame
	void Update()
	{
		if (damageThresholdSlider.value > currentHealthSlider.value)
		{
			amountOfDTSlider.transform.Find("Fill Area").gameObject.GetComponentInChildren<Image>().color = Color.yellow;
		}
		else {
			amountOfDTSlider.transform.Find("Fill Area").gameObject.GetComponentInChildren<Image>().color = Color.black;
		}

		//CH > DT and decreasing so start the animation to alert the user the health is decreasing
		if (currentHealthSlider.value > damageThresholdSlider.value) {
			currentHealthSlider.transform.Find ("Handle Slide Area").gameObject.SetActive (true); 
		} else {
			currentHealthSlider.transform.Find ("Handle Slide Area").gameObject.SetActive (false); 
		}

	}

	public void UpdateDamageThreshold(int newValue)
	{
		damageThresholdSlider.value = newValue;
		amountOfDTSlider.value = newValue;

		UpdateNonDecreasingHealthSlider ();
	}

	public void UpdateCurrentHealth(int newValue)
	{
		currentHealthSlider.value = newValue;

		UpdateNonDecreasingHealthSlider ();
	}

	private void UpdateNonDecreasingHealthSlider()
	{
		//Only want to see the "blue" Slider when the DT < CH 
		if (damageThresholdSlider.value < currentHealthSlider.value) {
			nonDecreasingHealthSlider.value = damageThresholdSlider.value;
		}
		else{
			nonDecreasingHealthSlider.value = currentHealthSlider.value;
		}
	}
}