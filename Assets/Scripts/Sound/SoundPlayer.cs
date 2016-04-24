using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    private const float LN10 = 2.302f;

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

        SetMusicVolume01(UIManager.musicVolume);
        SetSoundVolume01(UIManager.effectsVolume);
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
        percent = Mathf.Clamp(percent, 0.001f, 1);
        float amplitude = 20 * Mathf.Log(percent) / LN10;
        instance.SetMusicVolume(amplitude);
    }

    public static void SetSoundVolume01(float percent)
    {
        percent = Mathf.Clamp(percent, 0.001f, 1);
        float amplitude = 20 * Mathf.Log(percent) / LN10;
        instance.SetSoundVolume(amplitude);
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
        return Mathf.Pow(10, musicDB / 20);
    }

    public static float GetSoundPercent()
    {
        return Mathf.Pow(10, soundDB / 20);
    }

    public static AudioClip GetAudioClip(string clipName)
    {
        if (instance)
            foreach (var namedAudio in list)
                if (namedAudio.name == clipName)
                    return namedAudio.clip;

        return null;
    }

    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
        public float volume = 1;
    }
}