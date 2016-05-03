using UnityEngine;
using System.Collections;
using System;

public class ChainUI : MonoBehaviour
{
    private static ChainUI instance;
    private static CanvasGroup canvasGroup;
    private static GameObject chainOne;
    private static GameObject chainTwo;
    private static GameObject chainThree;
    private static GameObject chainFinish;
    private static bool fadeOut;
    private static float fadeTimer;
    private static float fadeDuration = 0.2f;

    // Use this for initialization
    void Start()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        chainOne = this.FindContainsInChildren("ChainOne");
        chainTwo = this.FindContainsInChildren("ChainTwo");
        chainThree = this.FindContainsInChildren("ChainThree");
        chainFinish = this.FindContainsInChildren("ChainFinish");

        chainOne.SetActive(false);
        chainTwo.SetActive(false);
        chainThree.SetActive(false);
        chainFinish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
            UpdateFade();
        else
            ResetFade();

    }

    private static void ResetFade()
    {
        if (canvasGroup) canvasGroup.alpha = 1;
        fadeTimer = 0;
    }

    private static void UpdateFade()
    {
        if (fadeDuration > 0)
        {
            fadeTimer += Time.deltaTime;
            fadeTimer = Mathf.Min(fadeTimer, fadeDuration);
            if (canvasGroup) canvasGroup.alpha = 1 - (fadeTimer / fadeDuration);
        }
        else
        {
            if (canvasGroup) canvasGroup.alpha = 0;
        }
    }

    public static void SetChainNumber(int chainNumber)
    {
        fadeOut = false;
        chainFinish.SetActive(false);

        if (chainNumber >= 3)
        {
            chainOne.SetActive(true);
            chainTwo.SetActive(true);
            chainThree.SetActive(true);
        }
        else if (chainNumber >= 2)
        {
            chainOne.SetActive(true);
            chainTwo.SetActive(true);
            chainThree.SetActive(false);
        }
        else if (chainNumber >= 1)
        {
            chainOne.SetActive(true);
            chainTwo.SetActive(false);
            chainThree.SetActive(false);
        }
        else
        {
            fadeOut = true;
        }
    }

    public static void AddHeavy()
    {
        chainFinish.SetActive(true);
    }
}
