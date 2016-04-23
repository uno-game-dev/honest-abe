using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrinketSlider : MonoBehaviour {

	public Slider trinketSlider;
	public Slider maryToddsSlider;

	// Use this for initialization
	void Start () {
		trinketSlider.value = 100;
		maryToddsSlider.value = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if ((PerkManager.activeTrinketPerk != null) && (Perk.trinketTimeStamp >= Time.time)) {
			trinketSlider.transform.FindContainsInChildren ("TrinketBackground").SetActive (true);
			trinketSlider.transform.FindContainsInChildren ("TrinketFillArea").SetActive (true);
		} else {
			PerkManager.updateTrinketBar = false;
			PerkManager.trinketTime = 100;
			trinketSlider.value = 100; //Reset
			trinketSlider.transform.FindContainsInChildren ("TrinketBackground").SetActive (false);
			trinketSlider.transform.FindContainsInChildren ("TrinketFillArea").SetActive (false);
		}

		if ((PerkManager.activeTrinketPerk != null) && (Perk.performMaryToddsTimeStamp >= Time.time)) {
			maryToddsSlider.transform.FindContainsInChildren("MaryToddsFillArea").SetActive(true);
		} else {
			PerkManager.updateMaryToddsBar = false;
			PerkManager.maryToddsTrinketTime = 100;
			maryToddsSlider.value = 100; //Reset
			maryToddsSlider.transform.FindContainsInChildren("MaryToddsFillArea").SetActive(false);
		}
	}

	public void UpdateTrinket(int newValue)
	{
		trinketSlider.value = newValue;
	}

	public void UpdateMaryToddsTrinket(int newValue)
	{
		maryToddsSlider.value = newValue;
	}

	public void Reset(int startValue){
		trinketSlider.maxValue = startValue;
		trinketSlider.value = startValue;
		maryToddsSlider.maxValue = startValue;
		maryToddsSlider.value = startValue;
	}
}