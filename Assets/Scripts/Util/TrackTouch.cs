using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TrackTouch : MonoBehaviour
{
    public GameObject TouchIDPrefab;
    private Dictionary<int,GameObject> touchIds = new Dictionary<int, GameObject>();
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            List<int> fingerIds = new List<int>();
            foreach (Touch touch in Input.touches)
                fingerIds.Add(touch.fingerId);

            //List<int> fingerIdsToRemove = new List<int>();
            //foreach (int fingerId in touchIds.Keys)
            //    if (!fingerIds.Contains(fingerId))
            //        fingerIdsToRemove.Add(fingerId);

            //foreach (int fingerId in fingerIdsToRemove)
            //{
            //    Destroy(touchIds[fingerId]);
            //    touchIds.Remove(fingerId);
            //}

            foreach (int fingerId in fingerIds)
                if (!touchIds.ContainsKey(fingerId))
                {
                    GameObject touchId = Instantiate(TouchIDPrefab);
                    touchId.transform.SetParent(transform);
                    touchIds.Add(fingerId, touchId);
                }

            foreach (Touch touch in Input.touches)
            {
                GameObject touchId = touchIds[touch.fingerId];
                touchId.transform.position = touch.position;
                touchId.GetComponentInChildren<Text>().text = string.Format("Touch {0}\n{1}", touch.fingerId, touch.phase);
            }
        }
        else
        {
            //foreach (int fingerId in touchIds.Keys)
            //    Destroy(touchIds[fingerId]);

            //touchIds.Clear();
        }
    }
}
