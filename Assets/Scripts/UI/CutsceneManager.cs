using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneManager : MonoBehaviour {

    public bool cutsceneActive; // Whether or not we're currently playing a cutscene
    public int index; // The "index" of the cutscene, i.e. which part of the cutscene we are currently in if it's multi-part

    // The list of our cutscenes in the game (there's three)
    public enum Cutscenes
    {
        NULL,
        INTRO,
        MID,
        END
    }
    public Cutscenes currentCutscene; // The cutscene that we're currently playing


    private string[] _introText = {
        "The last thing Abe can remember was a romantic carriage ride with Mary Todd, which was interrupted when they were ambushed by a group of confederates and General Robert E. Lee himself.",
        "Abe can still see the look on his face as the General aimed his pistol and fired."
    };

    private string[] _endText = {
        "With a sliver of hope that Mary Todd may yet be alive, Abe wanders off in search of his beloved, his bloody axe hungering for the next battle. "
    };

    public Sprite[] _midSprites;

    private bool _cutsceneOver;

    private GameObject _cutsceneCanvas; // The canvas object that is used for all the cutscenes
    private GameObject _introStoryPanel, _midStoryPanel, _endStoryPanel;
    private Text _introStoryText, _endStoryText;
    private Image _midStoryImage;

	void Start()
    {
        _cutsceneCanvas = GameObject.Find("CutsceneCanvas");

        _introStoryPanel = GameObject.Find("IntroCutscenePanel");
        _introStoryText = _introStoryPanel.transform.Find("Text").GetComponent<Text>();
        _introStoryText.text = _introText[0];
        _introStoryPanel.SetActive(false);

        _midStoryPanel = GameObject.Find("MidCutscenePanel");
        _midStoryImage = _midStoryPanel.transform.Find("Image").GetComponent<Image>();
        _midStoryPanel.SetActive(false);

        _endStoryPanel = GameObject.Find("EndCutscenePanel");
        _endStoryText = _endStoryPanel.transform.Find("Text").GetComponent<Text>();
        _endStoryText.text = _endText[0];
        _endStoryPanel.SetActive(false);

        _cutsceneCanvas.SetActive(false);

        currentCutscene = Cutscenes.NULL;
        //ChangeCutscene(Cutscenes.INTRO);
        _cutsceneOver = false;
        index = 0;
    }

    void Update()
    {
        if (currentCutscene == Cutscenes.NULL)
            return;

        if (Input.anyKeyDown)
        {
            index++;

            if (currentCutscene == Cutscenes.INTRO)
            {
                if (index >= _introText.Length)
                {
                    _cutsceneOver = true;
                }
                else if (index < _introText.Length)
                {
                    _introStoryText.text = _introText[index];
                }
            }
            else if (currentCutscene == Cutscenes.MID)
            {
                if (index >= _midSprites.Length)
                {
                    _cutsceneOver = true;
                }
                else if (index < _midSprites.Length)
                {
                    _midStoryImage.sprite = _midSprites[index];
                }
            }
            else if (currentCutscene == Cutscenes.END)
            {
                if (index >= _endText.Length)
                {
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
                _introStoryPanel.SetActive(true);
                break;
            case Cutscenes.MID:
                _midStoryPanel.SetActive(true);
                break;
            case Cutscenes.END:
                _endStoryPanel.SetActive(true);
                break;
            case Cutscenes.NULL:
                _cutsceneCanvas.SetActive(false);
                break;
        }
    }

    private void EndCutscene()
    {
        ResetCutscenes();

        _cutsceneCanvas.SetActive(false);

        currentCutscene = Cutscenes.NULL;

        _cutsceneOver = false;
    }

    private void ResetCutscenes()
    {
        index = 0;

        _introStoryPanel.SetActive(false);
        _introStoryText.text = _introText[index];

        _midStoryPanel.SetActive(false);
        _midStoryImage.sprite = _midSprites[index];

        _endStoryPanel.SetActive(false);
        _endStoryText.text = _endText[index];
    }
}
