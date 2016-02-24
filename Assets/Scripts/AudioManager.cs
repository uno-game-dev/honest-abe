using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
    public AudioClip introMusic;
    public AudioClip playMusic;

    public AudioClip[] AttackSounds = new AudioClip[2];
    public AudioClip[] DamageSounds = new AudioClip[2];

    public Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>(2);

    private static AudioSource CameraSource = null;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        CameraSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        initSoundLib();
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

    private void initSoundLib()
    {
        Sounds["Swing"] = AttackSounds[0];
   
    }

    public void PlaySound( string name)
    {
        if (Sounds.ContainsKey(name))
            AudioSource.PlayClipAtPoint(Sounds[name], CameraSource.transform.position, 0.7f);
    }

    public void PlayDamageSound( int index = 0)
    {
        AudioSource.PlayClipAtPoint(DamageSounds[index], CameraSource.transform.position, 0.7f);
    }

    public void PlayAttackSound(int index = 0)
    {
        AudioSource.PlayClipAtPoint(AttackSounds[index], CameraSource.transform.position, 2f);
    }
}
