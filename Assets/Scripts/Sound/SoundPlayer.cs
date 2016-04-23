using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [HideInInspector]
    public List<NamedAudioClip> namedAudioList = new List<NamedAudioClip>();
    private static SoundPlayer instance;
    private static List<NamedAudioClip> list;

    public static float musicDB;
    public static float soundDB;
    public AudioMixer masterMixer;
    public AudioMixerGroup soundMixerGroup;
    public AudioMixerGroup musicMixerGroup;

    private void Awake()
    {
        instance = this;
        list = namedAudioList;
    }

    public static GameObject Play(string clipName, bool loop = false)
    {
        List<NamedAudioClip> randomList = new List<NamedAudioClip>();
        foreach (var namedAudio in list)
            if (namedAudio.name == clipName)
                randomList.Add(namedAudio);

        if (randomList.Count > 0)
            return Play(randomList[Random.Range(0, randomList.Count)], loop);

        Debug.Log("Sound not found: " + clipName);
        return null;
    }

    public static GameObject Play(NamedAudioClip namedAudio, bool loop)
    {
        var sound = new GameObject(string.Format("Sound - {0}", namedAudio.name));
        sound.transform.SetParent(instance.transform);
        sound.transform.localPosition = Vector3.zero;
        var source = sound.AddComponent<AudioSource>();
        source.clip = namedAudio.clip;
        source.volume = namedAudio.volume;

        if (namedAudio.name.Contains("Music"))
            source.outputAudioMixerGroup = instance.musicMixerGroup;
        else
            source.outputAudioMixerGroup = instance.soundMixerGroup;

        source.Play();
        source.loop = loop;
        if (!loop) Destroy(sound, namedAudio.clip.length + 1);
        return sound;
    }

    public static void SetMusicVolume01(float percent)
    {
        instance.SetMusicVolume((Mathf.Clamp01(percent) * 80) + (-80));
    }

    public static void SetSoundVolume01(float percent)
    {
        instance.SetSoundVolume((Mathf.Clamp01(percent) * 80) + (-80));
    }

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

    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
        public float volume = 1;
    }
}