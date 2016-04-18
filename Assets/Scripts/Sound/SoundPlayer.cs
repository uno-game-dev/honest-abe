using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    public static float musicDB;
    public static float soundDB;
    public AudioMixer masterMixer;

    public void SetMusicVolume(float musicDB)
    {
        SoundPlayer.musicDB = musicDB;
        masterMixer.SetFloat("musicVolume", musicDB);
    }

    public void SetSoundVolume(float soundDB)
    {
        SoundPlayer.soundDB = soundDB;
        masterMixer.SetFloat("soundVolume", soundDB);
    }

    public static float GetMusicPercent()
    {
        return Mathf.Clamp01(80 + musicDB / 80);
    }

    public static float GetSoundPercent()
    {
        return Mathf.Clamp01(80 + soundDB / 80);
    }
}