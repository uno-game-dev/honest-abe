using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundPlayer : MonoBehaviour
{
    [HideInInspector]
    public List<NamedAudioClip> namedAudioList = new List<NamedAudioClip>();
    private static SoundPlayer instance;
    private static List<NamedAudioClip> list;

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
        source.Play();
        source.loop = loop;
        if (!loop) Destroy(sound, namedAudio.clip.length + 1);
        return sound;
    }

    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
        public float volume = 1;
    }
}