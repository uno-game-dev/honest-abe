using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TrackTouch : MonoBehaviour
{
    public GameObject TouchIDPrefab;
    public float fadeTime = 1;
    private Dictionary<int, GameObject> touchIds = new Dictionary<int, GameObject>();

    void Update()
    {
        List<int> fingerIds = new List<int>();
        if (Input.touchCount > 0)
        {
            fingerIds.AddRange(GetCurrentFingerIds());
            CreateNewFingerIdVisuals(fingerIds);
            PositionFingerIdVisuals();
        }
        CountDownFadeTimers(fingerIds);
    }

    private static List<int> GetCurrentFingerIds()
    {
        List<int> fingerIds = new List<int>();
        foreach (Touch touch in Input.touches)
            fingerIds.Add(touch.fingerId);
        return fingerIds;
    }

    private void CreateNewFingerIdVisuals(List<int> fingerIds)
    {
        foreach (int fingerId in fingerIds)
            if (!touchIds.ContainsKey(fingerId))
            {
                GameObject touchId = Instantiate(TouchIDPrefab);
                touchId.transform.SetParent(transform);
                touchIds.Add(fingerId, touchId);
            }
            else
            {
                touchIds[fingerId].GetComponentInChildren<Image>().color = Color.white;
                touchIds[fingerId].GetComponentInChildren<Text>().color = Color.white;
            }
    }

    private void PositionFingerIdVisuals()
    {
        foreach (Touch touch in Input.touches)
        {
            GameObject touchId = touchIds[touch.fingerId];
            touchId.transform.position = touch.position;
            touchId.GetComponentInChildren<Text>().text = string.Format("Touch {0}\n{1}", touch.fingerId, touch.phase);
        }
    }

    private void CountDownFadeTimers(List<int> fingerIds)
    {
        List<int> fingerIdsToRemove = new List<int>();
        foreach (int fingerId in touchIds.Keys)
            if (!fingerIds.Contains(fingerId))
                fingerIdsToRemove.Add(fingerId);

        foreach (int fingerId in fingerIdsToRemove)
            UpdateFade(fingerId);
    }

    private void UpdateFade(int fingerId)
    {
        Color newColor = touchIds[fingerId].GetComponentInChildren<Image>().color;
        newColor.a -= 1 / fadeTime * Time.deltaTime;
        if (newColor.a <= 0)
        {
            Destroy(touchIds[fingerId]);
            touchIds.Remove(fingerId);
            return;
        }
        touchIds[fingerId].GetComponentInChildren<Image>().color = newColor;
        touchIds[fingerId].GetComponentInChildren<Text>().color = newColor;
    }
}
