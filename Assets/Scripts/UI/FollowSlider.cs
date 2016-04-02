using UnityEngine;
using System.Collections;

public class FollowSlider : MonoBehaviour
{
    public RectTransform slider;
    public Vector3 sliderPos;
    public Vector3 sliderPosToWorld;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        sliderPos = slider.position;
        sliderPosToWorld = Camera.main.ScreenToWorldPoint(sliderPos);
        Vector2 pos = sliderPosToWorld + offset;
        transform.position = pos;
    }
}
