using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    
    public AudioClip introMusic;
    public AudioClip playMusic;

    private static AudioSource CameraSource = null;

	// Use this for initialization
	void Start () {
        CameraSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(UIManager.updateActive)
        {
            PlayGameMusic();
        }
        else
        {
            PlayIntroMusic();
        }
	}

    void PlayIntroMusic()
    {
        if (CameraSource.clip != introMusic)
        {
            CameraSource.clip = introMusic;
            CameraSource.Play();
        }
    }

    void PlayGameMusic()
    {
        if (CameraSource.clip != playMusic)
        {
            CameraSource.clip = playMusic;
            CameraSource.Play();
        }
    }
}
