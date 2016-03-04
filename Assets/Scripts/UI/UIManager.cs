using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static bool updateActive = false;
    public static bool displayLost;
    public static bool displayWin;
    [HideInInspector]
    public Text perkText;

    private bool _paused = false;
    private bool _options = false;
    private bool _slowMotion;
    private GameObject _startGameText;
    private GameObject _pauseUI;
    private GameObject __optionsUI;
    private GameObject _loseUI;
    private GameObject _winUI;

    void Start()
    {
        _startGameText = GameObject.Find("StartText");
        _pauseUI = GameObject.Find("PauseUI");
        __optionsUI = GameObject.Find("OptionsCanvas");
        _loseUI = GameObject.Find("LoseUI");
        _winUI = GameObject.Find("WinUI");
        perkText = GameObject.Find("PerkText").GetComponent<Text>();
        _pauseUI.SetActive(false);
        __optionsUI.SetActive(false);
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
        perkText.enabled = false;
        displayWin = false;
        displayLost = false;
        _slowMotion = true;
    }

    void Update()
    {
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

        if (displayLost)
        {
            _loseUI.SetActive(true);
        }

        if (displayWin)
        {
            _winUI.SetActive(true);
            StartCoroutine(LastKillSlowMo());
        }
    }

    IEnumerator LastKillSlowMo()
    {
        if (_slowMotion)
        {
            Time.timeScale = 0.2f; //Slow-mo for last kill
            yield return new WaitForSeconds(1.4f);
            _slowMotion = false;
        }
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _paused = false;
    }

    public void Options()
    {
        //Fill in functionality of the _options button in Pause menu
        _options = true;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        updateActive = false;
    }

    public void Quit()
    {
        Application.Quit(); //Only works when the project is built
    }

    public void Exit_options()
    {
        _options = false;
        _paused = true;
    }

    //After Losing
    public void RetryYes()
    {
        //Need to restart game or restart level depending on team, but for the alpha since it's only one scene it will restart the level
        Application.LoadLevel(Application.loadedLevel);
        _loseUI.SetActive(false);
        updateActive = false;
    }

    //After Losing
    public void RetryNo()
    {
        //Need to go back to the main menu, but for the alpha just quit the game
        Application.Quit(); //Only works when the project is built
    }

    //After Win
    public void PlayAgainYes()
    {
        //Need to restart the game, but for the alpha since it's only one scene it will restart the level
        Application.LoadLevel(Application.loadedLevel);
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
