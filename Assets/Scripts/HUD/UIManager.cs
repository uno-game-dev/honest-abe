using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static bool updateActive = false;
	private bool paused = false;
	private bool options = false;
	private bool slowMotion;
	public static bool displayLost;
	public static bool displayWin;
	private GameObject startGameText;
	private GameObject pauseUI;
	private GameObject optionsUI;
	private GameObject loseUI;
	private GameObject winUI;
    [HideInInspector] public Text perkText;

    void Start() {
		startGameText = GameObject.Find("StartText");
		pauseUI = GameObject.Find("PauseUI");
		optionsUI = GameObject.Find("OptionsCanvas");
		loseUI = GameObject.Find("LoseUI");
		winUI = GameObject.Find ("WinUI");
        perkText = GameObject.Find("PerkText").GetComponent<Text>();
        pauseUI.SetActive (false);
		optionsUI.SetActive (false);
		loseUI.SetActive (false);
		winUI.SetActive (false);
        perkText.enabled = false;
		displayWin = false;
		displayLost = false;
		slowMotion = true;
	}

	void Update () {
		if (!updateActive && Input.GetKeyDown(KeyCode.Return)) {
			updateActive = true;
			startGameText.SetActive(false);
		}

		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
		}

		if (paused) {
			if (options) {
				pauseUI.SetActive (false);
				optionsUI.SetActive (true);
			} else {
				pauseUI.SetActive (true);
				optionsUI.SetActive(false);
				Time.timeScale = 0;
			}
		}
		else {
			pauseUI.SetActive (false);
			Time.timeScale = 1;
		}

		if (displayLost) {
			loseUI.SetActive (true);
		}

		if (displayWin) {
			winUI.SetActive (true);
			StartCoroutine (LastKillSlowMo());
		}
	}

	IEnumerator LastKillSlowMo(){
		if (slowMotion) {
			Time.timeScale = 0.2f; //Slow-mo for last kill
			yield return new WaitForSeconds (1.4f);
			slowMotion = false;
		}
		Time.timeScale = 0;
	}

	public void Resume(){
		paused = false;
	}

	public void Options(){
		//Fill in functionality of the options button in Pause menu
		options = true;
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
		updateActive = false;
	}

	public void Quit(){
		Application.Quit(); //Only works when the project is built
	}

	public void ExitOptions (){
		options = false;
		paused = true;
	}

	//After Losing
	public void RetryYes(){
		//Need to restart game or restart level depending on team, but for the alpha since it's only one scene it will restart the level
		Application.LoadLevel (Application.loadedLevel);
		loseUI.SetActive (false);
		updateActive = false;
	}

	//After Losing
	public void RetryNo(){
		//Need to go back to the main menu, but for the alpha just quit the game
		Application.Quit(); //Only works when the project is built
	}

	//After Win
	public void PlayAgainYes(){
		//Need to restart the game, but for the alpha since it's only one scene it will restart the level
		Application.LoadLevel (Application.loadedLevel);
		winUI.SetActive(false);
		updateActive = false;
	}

	//After Win
	public void PlayAgainNo(){
		//Need to go back to the main menu, but for the alpha just quit the game
		Application.Quit(); //Only works when the project is built	
	}
}
