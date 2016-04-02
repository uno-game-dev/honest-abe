using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour
{
    public float swipeTime = 1;
    private float swipeTimer;

    // Update is called once per frame
    void Update()
    {
        swipeTimer += Time.deltaTime;

        if (swipeTimer >= swipeTime)
            gameObject.SetActive(false);
    }

    public void Activate()
    {
        swipeTimer = 0;
        gameObject.SetActive(true);
    }
}
