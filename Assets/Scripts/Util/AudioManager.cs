﻿using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;
    public AudioClip introMusic;
    public AudioClip playMusic;

    //public AudioClip[] AttackSounds = new AudioClip[2];
    //public AudioClip[] DamageSounds = new AudioClip[2];
    //public AudioClip[] GenericSounds = new AudioClip[4];
    //public List<AudioClip> BossSounds = new List<AudioClip>();
    //public List<AudioClip> ItemSounds = new List<AudioClip>();
    //public List<AudioClip> WeaponSounds = new List<AudioClip>();

	[System.Serializable]
	public struct SoundEntry {
		public string name;
		public AudioClip sound;
	}

	public SoundEntry[] SoundArray;

    public Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();

    private static AudioSource CameraSource = null;
    private static AudioSource source = null;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        source = this.gameObject.GetComponent<AudioSource>();
        CameraSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        source.volume = 1;
        initSoundLib();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.updateActive)
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
		foreach (SoundEntry pair in SoundArray) {
			Sounds.Add( pair.name, pair.sound);
		}

    }

    public void PlaySound(string name)
    {
        if (Sounds.ContainsKey(name))
            AudioSource.PlayClipAtPoint(Sounds[name], CameraSource.transform.position, 0.7f);
    }

	/*
    public void PlayDamageSound(int index = 0)
    {
        if (index == 0)
            AudioSource.PlayClipAtPoint(DamageSounds[index], CameraSource.transform.position, 0.7f);
        else
        {
            source.clip = DamageSounds[index];
            source.Play();
        }
    }

    public void PlayAttackSound(int index = 0, float timeDelay = 0f)
    {
        source.clip = AttackSounds[index];
        source.PlayDelayed(timeDelay);
        //AudioSource.PlayClipAtPoint(AttackSounds[index], CameraSource.transform.position, 2f);
    }

    public void PlayGenericSound(int index = 0, float timeDelay = 0f)
    {
        source.clip = GenericSounds[index];
        source.PlayDelayed(timeDelay);
    }

    public void PlayBossSound(int index = 0, float timeDelay = 0f)
    {
        source.clip = BossSounds[index];
        source.PlayDelayed(timeDelay);
    }

    public void PlayItemSound(int index = 0, float timeDelay = 0f)
    {
        source.clip = ItemSounds[index];
        source.PlayDelayed(timeDelay);
    }

	public void PlayWeaponSound(int index = 0, float timeDelay = 0f)
    {
        source.clip = WeaponSounds[index];
        source.PlayDelayed(timeDelay);
    }
    */
}
