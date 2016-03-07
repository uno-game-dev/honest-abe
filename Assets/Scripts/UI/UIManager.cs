using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static bool updateActive = false;
    [HideInInspector]
    public Text perkText;

    private bool _paused = false;
    private bool _options = false;
    private GameObject _startGameText;
    private GameObject _pauseUI;
    private GameObject __optionsUI;
    private GameObject _loseUI;
    private GameObject _winUI;
	private GameManager _gameManager;
	private LevelManager _levelManager;

	void Awake()
    {
		updateActive = false;	
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        _startGameText = GameObject.Find("StartText");
        _pauseUI = GameObject.Find("PauseUI");
        __optionsUI = GameObject.Find("OptionsCanvas");
        _loseUI = GameObject.Find("LoseUI");
        _winUI = GameObject.Find("WinUI");
        perkText = GameObject.Find("PerkText").GetComponent<Text>();
		_startGameText.SetActive(true);
		_pauseUI.SetActive(false);
        __optionsUI.SetActive(false);
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
        perkText.enabled = false;
    }

    void Update()
    {
		if (_gameManager.win)
		{
			_winUI.SetActive(true);
		}

		if (_gameManager.lose)
		{
			_loseUI.SetActive(true);
		}

        if (!updateActive && Input.GetKeyDown(KeyCode.Return))
        {
            updateActive = true;
            _startGameText.SetActive(false);
        }

        if (Input.GetButtonDown("Pause"))
        {
            _paused = !_paused;
        }

        if (_paused)
        {
            if (_options)
            {
                _pauseUI.SetActive(false);
                __optionsUI.SetActive(true);
            }
            else {
                _pauseUI.SetActive(true);
                __optionsUI.SetActive(false);
                Time.timeScale = 0;
            }
        }
        else {
            _pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnResume()
    {
        _paused = false;
    }

    public void OnOptions()
    {
        //Fill in functionality of the _options button in Pause menu
        _options = true;
    }

    public void OnRestart()
    {
		_levelManager.loadCurrentLevel();
        updateActive = false;
    }

    public void OnQuit()
    {
        Application.Quit(); //Only works when the project is built
    }

    public void OnOptionsBack()
    {
        _options = false;
        _paused = true;
    }

    //After Losing
    public void OnRetryYes()
    {
		//Need to restart game or restart level depending on team, but for the alpha since it's only one scene it will restart the level
		_levelManager.loadCurrentLevel();
		_loseUI.SetActive(false);
        updateActive = false;
    }

    //After Losing
    public void OnRetryNo()
    {
        //Need to go back to the main menu, but for the alpha just quit the game
        Application.Quit(); //Only works when the project is built
    }

    //After Win
    public void PlayAgainYes()
    {
		//Need to restart the game, but for the alpha since it's only one scene it will restart the level
		_levelManager.loadCurrentLevel();
		_winUI.SetActive(false);
        updateActive = false;
    }

    //After Win
    public void PlayAgainNo()
    {
        //Need to go back to the main menu, but for the alpha just quit the game
        Application.Quit(); //Only works when the project is built	
    }
}
