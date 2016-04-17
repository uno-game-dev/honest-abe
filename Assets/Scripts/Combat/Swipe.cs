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

    public void Activate(bool flipped = false)
    {
        swipeTimer = 0;
        gameObject.SetActive(true);

        Vector3 scale = transform.localScale;
        if (flipped) scale.z = -1;
        else scale.z = 1;
        transform.localScale = scale;
    }
}
