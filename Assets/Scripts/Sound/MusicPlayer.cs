using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public GameObject music;
    private static MusicPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Play("Introduction Music");
    }

    public static void Play(string song)
    {
        if (instance.music) Destroy(instance.music);
        instance.music = SoundPlayer.Play(song, true);
    }
}
