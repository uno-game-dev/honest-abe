using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static bool updateActive = false;

    public static string MusicVolume = "MusicVolume", EffectsVolume = "EffectsVolume";
    public static float musicVolume, effectsVolume;

    [HideInInspector]
    public Text perkText;

	// Boss UI
	[HideInInspector]
	public Canvas bossHealthUI;
    public Canvas optionButtonUI;

	private GameManager _gameManager;
	private LevelManager _levelManager;
	private GameObject _startGameText;
	private bool _paused = false;
	private bool _options = false;

	// Pause UI
	private GameObject _pauseUI;
	private Button _pauseUIResumeButton;
	private Button _pauseUIOptionsButton;
	private Button _pauseUIQuitButton;

	// Options UI
	private GameObject _optionsUI, _optionsUIBackground;
	private Button _optionsUIBackButton;
    private Button _optionsUIGraphicsButton;
    private Button _optionsUIAudioButton;
    private Button _optionsUIControlsButton;

    // Graphics Menu
    private GameObject _graphicsUI;
    private Button _graphicsUIBackButton, _graphicsUIApplyButton;
    private Toggle _graphicsUIFullscreenToggle;
    private Dropdown _graphicsUIResList;

    // Audio Menu
    private GameObject _audioUI;
    private Button _audioUIBackButton, _audioUIApplyButton;
    private Slider _audioUIMusicSlider, _audioUIEffectsSlider;
    private Text _audioUIMusicValue, _audioUIEffectValue;

    // Controls Menu
    private GameObject _controlsUI;
    private Button _controlsUIBackButton;

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
		_maryToddsLocketteUI = GameObject.Find ("MaryToddsLocketteText").GetComponent<Text>();
		_maryToddsLocketteUI.enabled = false;
    }

    void Update()
    {
		if (!updateActive && Input.GetKeyDown(KeyCode.Return))
            OnPressEnterAtBeginning();

        if (Input.GetButtonDown("Pause"))
            TogglePause();

        _audioUIMusicValue.text = "" + (System.Math.Round(_audioUIMusicSlider.value * 100));
        _audioUIEffectValue.text = "" + (System.Math.Round(_audioUIEffectsSlider.value * 100));

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

    public void TogglePause()
    {
        _paused = !_paused;

        if (_paused)
        {
            _pauseUI.SetActive(true);
            _optionsUI.SetActive(false);
            _optionsUIBackground.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            _pauseUI.SetActive(false);
            _optionsUI.SetActive(false);
            _optionsUIBackground.SetActive(false);
            Time.timeScale = 1;
        }
    }

	public void ActivateLoseUI()
	{
		_loseUI.SetActive(true);
	}

    public void OnPressEnterAtBeginning()
    {
        updateActive = true;
        _startGameText.SetActive(false);
        optionButtonUI.gameObject.SetActive(true);
    }

	private void SetListenersForPauseUI()
	{
		_pauseUI = GameObject.Find("PauseUI");

		_pauseUIResumeButton = _pauseUI.transform.Find("Resume").GetComponent<Button>();
		_pauseUIOptionsButton = _pauseUI.transform.Find("Options").GetComponent<Button>();
		_pauseUIQuitButton = _pauseUI.transform.Find("Quit").GetComponent<Button>();

		_pauseUIResumeButton.onClick.AddListener(OnResume);
		_pauseUIOptionsButton.onClick.AddListener(OnOptions);
		_pauseUIQuitButton.onClick.AddListener(OnQuit);

        _pauseUI.SetActive(false);
	}

	private void SetListenersForOptionsUI()
	{
        _optionsUIBackground = GameObject.Find("OptionsBackground");
        _optionsUIBackground.SetActive(false);

		_optionsUI = GameObject.Find("OptionsUI");
		_optionsUIBackButton = _optionsUI.transform.Find("Back").GetComponent<Button>();
        _optionsUIAudioButton = _optionsUI.transform.Find("AudioBtn").GetComponent<Button>();
        _optionsUIControlsButton = _optionsUI.transform.Find("ControlsBtn").GetComponent<Button>();
        _optionsUIGraphicsButton = _optionsUI.transform.Find("GraphicsBtn").GetComponent<Button>();
		_optionsUIBackButton.onClick.AddListener(OnOptionsBack);
        _optionsUIGraphicsButton.onClick.AddListener(OnGraphics);
        _optionsUIAudioButton.onClick.AddListener(OnAudio);
        _optionsUIControlsButton.onClick.AddListener(OnControls);
        _optionsUI.SetActive(false);


        /*
         * Graphics
         */
        _graphicsUI = GameObject.Find("GraphicsUI");
        _graphicsUIBackButton = _graphicsUI.transform.Find("Back").GetComponent<Button>();
        _graphicsUIApplyButton = _graphicsUI.transform.Find("ApplyBtn").GetComponent<Button>();
        _graphicsUIFullscreenToggle = _graphicsUI.transform.Find("FullscreenToggle").GetComponent<Toggle>();
        _graphicsUIFullscreenToggle.isOn = Screen.fullScreen;
        _graphicsUIResList = _graphicsUI.transform.Find("ResList").GetComponent<Dropdown>();

        // Resolution List
        _graphicsUIResList.options.Clear();
        foreach (Resolution r in Screen.resolutions)
            _graphicsUIResList.options.Add(new Dropdown.OptionData(r.width + " x " + r.height));
        _graphicsUIResList.value = _graphicsUIResList.options.Count;
        _graphicsUIResList.value = 0;
        foreach (Resolution r in Screen.resolutions)
            if (Screen.width == r.width && Screen.height == r.height)
                _graphicsUIResList.value = System.Array.IndexOf(Screen.resolutions, r);
        // End Resolution List

        _graphicsUIBackButton.onClick.AddListener(OnGraphicsBack);
        _graphicsUIApplyButton.onClick.AddListener(OnGraphicsApply);
        _graphicsUI.SetActive(false);


        /*
         * Audio
         */
        _audioUI = GameObject.Find("AudioUI");
        _audioUIBackButton = _audioUI.transform.Find("Back").GetComponent<Button>();
        _audioUIApplyButton = _audioUI.transform.Find("ApplyBtn").GetComponent<Button>();
        _audioUIBackButton.onClick.AddListener(OnAudioBack);
        _audioUIApplyButton.onClick.AddListener(OnAudioApply);
        _audioUIMusicSlider = _audioUI.transform.Find("MusicSlider").GetComponent<Slider>();
        _audioUIEffectsSlider = _audioUI.transform.Find("FXSlider").GetComponent<Slider>();
        _audioUIMusicSlider.value = musicVolume;
        _audioUIEffectsSlider.value = effectsVolume;
        _audioUIMusicValue = _audioUI.transform.Find("MusicVolumeValue").GetComponent<Text>();
        _audioUIEffectValue = _audioUI.transform.Find("EffectVolumeValue").GetComponent<Text>();
        _audioUIMusicValue.text = "" + System.Math.Round(musicVolume, 1);
        _audioUIEffectValue.text = "" + System.Math.Round(effectsVolume, 1);
        _audioUI.SetActive(false);


        /*
         * Controls
         */
        _controlsUI = GameObject.Find("ControlsUI");
        _controlsUIBackButton = _controlsUI.transform.Find("Back").GetComponent<Button>();
        _controlsUIBackButton.onClick.AddListener(OnControlsBack);
        _controlsUI.SetActive(false);
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
        //_paused = false;
        _pauseUI.SetActive(false);
        Time.timeScale = 1;
        _paused = false;
        _optionsUIBackground.SetActive(false);
    }

    public void OnOptions()
    {
        //Fill in functionality of the _options button in Pause menu
        //_options = true;
        _pauseUI.SetActive(false);
        _optionsUI.SetActive(true);
        _optionsUIBackground.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit(); //Only works when the project is built
    }

    public void OnOptionsBack()
    {
        //_options = false;
        //_paused = true;
        _pauseUI.SetActive(true);
        _optionsUI.SetActive(false);
        _optionsUIBackground.SetActive(false);
        Time.timeScale = 0;
    }

    // When clicking the graphics button on the options menu
    public void OnGraphics()
    {
        _optionsUI.SetActive(false);
        _graphicsUI.SetActive(true);
    }

    // When clicking the audio button on the options menu
    public void OnAudio()
    {
        _optionsUI.SetActive(false);
        _audioUI.SetActive(true);
    }

    // When clicking the controls button on the options menu
    public void OnControls()
    {
        _optionsUI.SetActive(false);
        _controlsUI.SetActive(true);
    }

    // When clicking the back button on the graphics menu
    public void OnGraphicsBack()
    {
        _optionsUI.SetActive(true);
        _graphicsUI.SetActive(false);
    }

    // When clicking the back button on the audio menu
    public void OnAudioBack()
    {
        _audioUIMusicSlider.value = musicVolume;
        _audioUIEffectsSlider.value = effectsVolume;
        _optionsUI.SetActive(true);
        _audioUI.SetActive(false);
    }

    // When clicking the back button on the controls menu
    public void OnControlsBack()
    {
        _optionsUI.SetActive(true);
        _controlsUI.SetActive(false);
    }

    // When clicking the apply button on the graphics menu
    public void OnGraphicsApply()
    {
        // Apply the new resolution
        Screen.SetResolution(Screen.resolutions[_graphicsUIResList.value].width, Screen.resolutions[_graphicsUIResList.value].height, _graphicsUIFullscreenToggle.isOn);

        // Make sure the camera is positioned over the player appropriately
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().FixAspectRatio();
    }

    // When clicking the apply button on the audio menu
    public void OnAudioApply()
    {
        Debug.Log(_audioUIMusicSlider.value);
        Debug.Log(_audioUIEffectsSlider.value);

        musicVolume = _audioUIMusicSlider.value;
        effectsVolume = _audioUIEffectsSlider.value;

        PlayerPrefs.SetFloat(MusicVolume, musicVolume);
        PlayerPrefs.SetFloat(EffectsVolume, effectsVolume);
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
