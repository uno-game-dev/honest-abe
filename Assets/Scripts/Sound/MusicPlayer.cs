using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public GameObject music;
    public LevelManager levelManager;
    private static MusicPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelManager[] levelManagers = FindObjectsOfType<LevelManager>();
        LevelManager levelManager = levelManagers[0];
        foreach (LevelManager nextLevelManager in levelManagers)
            if (nextLevelManager.currentScene > levelManager.currentScene)
                levelManager = nextLevelManager;

        if (levelManager.currentScene == 0)
            Play("Introduction Music");
        else if (levelManager.currentScene == 1)
            Play("Forest Level Music");
        else if (levelManager.currentScene == 2)
            Play("BattleField Music");
        else if (levelManager.currentScene == 3)
            Play("Ballroom Music");
    }

    public static void Play(string song)
    {
        if (instance.music) Destroy(instance.music);
        instance.music = SoundPlayer.Play(song, true);
    }

    public static void Play(string introSong, string loopedSong)
    {
        if (instance.music) Destroy(instance.music);
        instance.music = SoundPlayer.Play(introSong);

        float duration = SoundPlayer.GetAudioClip(introSong).length;
        instance.StartCoroutine(instance.PlayLoopedSong(loopedSong, duration));
    }

    private IEnumerator PlayLoopedSong(string loopedSong, float delay)
    {
        yield return new WaitForSeconds(delay);
        Play(loopedSong);
    }
}
