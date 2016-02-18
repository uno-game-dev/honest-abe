using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static bool updateActive = false;
	private bool paused = false;
	private bool options = false;
	private GameObject startGameText;
	private GameObject pauseUI;
	private GameObject optionsUI;

	void Start() {
		startGameText = GameObject.Find("StartText");
		pauseUI = GameObject.Find("PauseUI");
		optionsUI = GameObject.Find("OptionsCanvas");
		pauseUI.SetActive (false);
		optionsUI.SetActive (false);
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
}
