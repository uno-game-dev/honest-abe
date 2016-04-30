using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{

    public static bool cutsceneActive; // Whether or not we're currently playing a cutscene
    public int index; // The "index" of the cutscene, i.e. which part of the cutscene we are currently in if it's multi-part

    // The list of our cutscenes in the game (there's three)
    public enum Cutscenes
    {
        NULL,
        INTRO,
        BEAR,
        MID,
        END
    }
    public Cutscenes currentCutscene; // The cutscene that we're currently playing


    private string[] _introText = {
        "Abe awakes, a chilling memory gradually coming back to him... A romantic carriage ride with Mary Todd… A confederate ambush...",
        "General Robert E Lee himself had stepped out of the shadows and aimed a pistol at Abe’s face...",
        "The last thing Abe can remember is the sound of that gunshot."
    };

    private string[] _bearText =
    {
        "Finally, Abe lumbers out of the forest they had tried to bury him in.",
        "It seems this battle has only just begun."
    };

    private string[] _endText = {
        "With a sliver of hope that Mary Todd may yet be alive, Abe wanders off in search of his beloved, his bloody axe hungering for the next battle."
    };

    private bool _cutsceneOver, _allowSkip;
    private float timeToAllowSkip = 2f, timer = 0f;

    private GameObject _cutsceneCanvas; // The canvas object that is used for all the cutscenes
    private GameObject _introStoryPanel, _bearStoryPanel, _midStoryPanel, _endStoryPanel, _skipText;
    private Text _introStoryText, _bearStoryText, _endStoryText;
    private Image _midStoryImage;

    void Start()
    {
        _cutsceneCanvas = GameObject.Find("CutsceneCanvas");

        _introStoryPanel = GameObject.Find("IntroCutscenePanel");
        _introStoryText = _introStoryPanel.transform.Find("Text").GetComponent<Text>();
        _introStoryText.text = _introText[0];
        _introStoryPanel.SetActive(false);

        _bearStoryPanel = GameObject.Find("BearCutscenePanel");
        _bearStoryText = _bearStoryPanel.transform.Find("Text").GetComponent<Text>();
        _bearStoryText.text = _bearText[0];
        _bearStoryPanel.SetActive(false);

        _midStoryPanel = GameObject.Find("MidCutscenePanel");
        _midStoryImage = _midStoryPanel.transform.Find("Image").GetComponent<Image>();
        _midStoryPanel.SetActive(false);

        _endStoryPanel = GameObject.Find("EndCutscenePanel");
        _endStoryText = _endStoryPanel.transform.Find("Text").GetComponent<Text>();
        _endStoryText.text = _endText[0];
        _endStoryPanel.SetActive(false);

        _skipText = GameObject.Find("SkipText");
        _skipText.SetActive(false);
        _allowSkip = false;

        _cutsceneOver = false;
        cutsceneActive = false;
        index = 0;

        ChangeCutscene(Cutscenes.NULL);
    }

    void Update()
    {
        if (currentCutscene == Cutscenes.NULL)
            return;

        timer += Time.deltaTime;

        if (timer >= timeToAllowSkip)
        {
            _skipText.SetActive(true);
            _allowSkip = true;
        }

        if ((Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)) && Time.timeScale > 0 && _allowSkip)
        {
            index++;
            timer = 0;
            _skipText.SetActive(false);
            _allowSkip = false;

            if (currentCutscene == Cutscenes.INTRO)
            {
                if (index >= _introText.Length)
                {
                    _cutsceneOver = true;
                    GameObject.Find("GameManager").GetComponent<PerkManager>().showInstructions = true;
                }
                else if (index < _introText.Length)
                {
                    _introStoryText.text = _introText[index];
                }
            }
            else if (currentCutscene == Cutscenes.BEAR)
            {
                if (index >= _bearText.Length)
                {
                    EventHandler.SendEvent(EventHandler.Events.LEVEL_NEXT);
                    _cutsceneOver = true;
                }
                else if (index < _bearText.Length)
                {
                    _bearStoryText.text = _bearText[index];
                }
            }
            else if (currentCutscene == Cutscenes.MID)
            {
                EventHandler.SendEvent(EventHandler.Events.LEVEL_NEXT);
                _cutsceneOver = true;
            }
            else if (currentCutscene == Cutscenes.END)
            {
                if (index >= _endText.Length)
                {
                    //GameObject.Find("GameManager").GetComponent<LevelManager>().LoadFirstLevel();
                    //UIManager.updateActive = false;
                    GameObject.Find("UI").GetComponent<UIManager>().WinUI.SetActive(true);
                    _cutsceneOver = true;
                }
                else if (index < _endText.Length)
                {
                    _endStoryText.text = _endText[index];
                }
            }
        }

        if (_cutsceneOver)
            EndCutscene();
    }

    public void ChangeCutscene(Cutscenes cutscene)
    {
        ResetCutscenes();
        currentCutscene = cutscene;
        _cutsceneCanvas.SetActive(true);

        switch (currentCutscene)
        {
            case Cutscenes.INTRO:
                cutsceneActive = true;
                _introStoryPanel.SetActive(true);
                GameObject.Find("Player").GetComponent<Cinematic>().cinematic = "Abe Rises";
                GameObject.Find("Player").GetComponent<Cinematic>().enabled = true;
                GameObject.Find("Player").GetComponent<PlayerHealth>().RefillForCutscene();
                break;
            case Cutscenes.BEAR:
                cutsceneActive = true;
                _bearStoryPanel.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerHealth>().RefillForCutscene();
                break;
            case Cutscenes.MID:
                cutsceneActive = true;
                _midStoryPanel.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerHealth>().RefillForCutscene();
                break;
            case Cutscenes.END:
                cutsceneActive = true;
                _endStoryPanel.SetActive(true);
                GameObject.Find("Player").GetComponent<Player>().PlayEnding();
                GameObject.Find("Player").GetComponent<PlayerHealth>().RefillForCutscene();
                break;
            case Cutscenes.NULL:
                cutsceneActive = false;
                _cutsceneCanvas.SetActive(false);
                break;
        }
    }

    private void EndCutscene()
    {
        ChangeCutscene(Cutscenes.NULL);
        _cutsceneOver = false;
    }

    private void ResetCutscenes()
    {
        index = 0;
        timer = 0;
        _allowSkip = false;

        _introStoryPanel.SetActive(false);
        _introStoryText.text = _introText[index];

        _bearStoryPanel.SetActive(false);
        _bearStoryText.text = _bearText[index];

        _midStoryPanel.SetActive(false);

        _endStoryPanel.SetActive(false);
        _endStoryText.text = _endText[index];
    }
}
