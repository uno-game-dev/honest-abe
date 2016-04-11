using UnityEngine;
using System.Collections;

public class SoundPlayerFollowListener : MonoBehaviour
{
    public GameObject following;

    // Use this for initialization
    void Start()
    {
        following = FindObjectOfType<AudioListener>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
            transform.position = following.transform.position;
    }
}
