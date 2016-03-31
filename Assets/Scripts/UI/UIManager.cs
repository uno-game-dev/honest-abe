using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static bool updateActive = false;

    [HideInInspector]
    public Text perkText;

	// Boss UI
	[HideInInspector]
	public Canvas bossHealthUI;

	private GameManager _gameManager;
	private LevelManager _levelManager;
	private GameObject _startGameText;
	private bool _paused = false;
	private bool _options = false;

	// Pause UI
	private GameObject _pauseUI;
	private Button _pauseUIResumeButton;
	private Button _pauseUIOptionsButton;
	private Button _pauseUIRestartButton;
	private Button _pauseUIQuitButton;

	// Options UI
	private GameObject _optionsUI;
	private Button _optionsUIBackButton;

	// Win UI
	private GameObject _winUI;
	private Button _winUIYesButton;
	private Button _winUINoButton;
	
	// Lose UI
	private GameObject _loseUI;
	private Button _loseUIYesButton;
	private Button _loseUINoButton;

	//Trinket UI
	private static Text _trinketUI;
	private static Text _maryToddsLocketteUI;

	void Awake()
	{
		updateActive = false;
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
		_startGameText = GameObject.Find("StartText");
		_startGameText.SetActive(true);
		SetListenersForPauseUI();
		SetListenersForOptionsUI();
		SetListenersForWinUI();
		SetListenersForLoseUI();
		perkText = GameObject.Find("PerkText").GetComponent<Text>();
        perkText.enabled = false;
		bossHealthUI = GameObject.Find("BossHUDMarkerCanvas").GetComponent<Canvas>();
		bossHealthUI.enabled = false;
		_trinketUI = GameObject.Find("ActivateTrinketText").GetComponent<Text>();
		_trinketUI.enabled = false;
		_maryToddsLocketteUI = GameObject.Find ("MaryToddsLocketteText").GetComponent<Text> ();
		_maryToddsLocketteUI.enabled = false;
    }

    void Update()
    {
		if (GlobalSettings.loseCondition)
			_loseUI.SetActive(true);
		if (!updateActive && Input.GetKeyDown(KeyCode.Return))
        {
            updateActive = true;
            _startGameText.SetActive(false);
        }

        if (Input.GetButtonDown("Pause"))
            _paused = !_paused;

        if (_paused)
        {
            if (_options)
            {
                _pauseUI.SetActive(false);
                _optionsUI.SetActive(true);
            }
            else {
                _pauseUI.SetActive(true);
                _optionsUI.SetActive(false);
                Time.timeScale = 0;
            }
        }
        else {
            _pauseUI.SetActive(false);
            Time.timeScale = 1;
        }

		if ((PerkManager.activeTrinketPerk != null) && (Perk.trinketTimeStamp <= Time.time)) {
			_trinketUI.enabled = true;
		} else {
			_trinketUI.enabled = false;
		}

		if ((PerkManager.activeTrinketPerk != null) && (Perk.performMaryToddsTimeStamp >= Time.time)) {
			_maryToddsLocketteUI.enabled = true;
		} else {
			_maryToddsLocketteUI.enabled = false;
		}
    }

    public void OnPressEnterAtBeginning()
    {
        updateActive = true;
        _startGameText.SetActive(false);
    }

	private void SetListenersForPauseUI()
	{
		_pauseUI = GameObject.Find("PauseUI");

		_pauseUIResumeButton = _pauseUI.transform.Find("Resume").GetComponent<Button>();
		_pauseUIOptionsButton = _pauseUI.transform.Find("Options").GetComponent<Button>();
		_pauseUIRestartButton = _pauseUI.transform.Find("Restart").GetComponent<Button>();
		_pauseUIQuitButton = _pauseUI.transform.Find("Quit").GetComponent<Button>();

		_pauseUIResumeButton.onClick.AddListener(OnResume);
		_pauseUIOptionsButton.onClick.AddListener(OnOptions);
		_pauseUIRestartButton.onClick.AddListener(OnRestart);
		_pauseUIQuitButton.onClick.AddListener(OnQuit);
	}

	private void SetListenersForOptionsUI()
	{
		_optionsUI = GameObject.Find("OptionsUI");

		_optionsUIBackButton = _optionsUI.transform.Find("Back").GetComponent<Button>();

		_optionsUIBackButton.onClick.AddListener(OnOptionsBack);

		_optionsUI.SetActive(false);
	}

	private void SetListenersForWinUI()
	{
		_winUI = GameObject.Find("WinUI");

		_winUIYesButton = _winUI.transform.Find("Yes").GetComponent<Button>();
		_winUINoButton = _winUI.transform.Find("No").GetComponent<Button>();

		_winUIYesButton.onClick.AddListener(OnWinYes);
		_winUINoButton.onClick.AddListener(OnWinNo);

		_winUI.SetActive(false);
	}

	private void SetListenersForLoseUI()
	{
		_loseUI = GameObject.Find("LoseUI");

		_loseUIYesButton = _loseUI.transform.Find("Yes").GetComponent<Button>();
		_loseUINoButton = _loseUI.transform.Find("No").GetComponent<Button>();

		_loseUIYesButton.onClick.AddListener(OnLoseYes);
		_loseUINoButton.onClick.AddListener(OnLoseNo);

		_loseUI.SetActive(false);
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
    public void OnLoseYes()
    {
		//Need to restart game or restart level depending on team, but for the alpha since it's only one scene it will restart the level
		_levelManager.loadCurrentLevel();
		_loseUI.SetActive(false);
        updateActive = false;
    }

    //After Losing
    public void OnLoseNo()
    {
        //Need to go back to the main menu, but for the alpha just quit the game
        Application.Quit(); //Only works when the project is built
    }

    //After Win
    public void OnWinYes()
    {
		//Need to restart the game, but for the alpha since it's only one scene it will restart the level
		_levelManager.loadCurrentLevel();
		_winUI.SetActive(false);
        updateActive = false;
    }

    //After Win
    public void OnWinNo()
    {
        //Need to go back to the main menu, but for the alpha just quit the game
        Application.Quit(); //Only works when the project is built	
    }
}
