using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public GameObject music;
    public LevelManager levelManager;
    public WorldGenerator worldGenerator;
    private static MusicPlayer instance;
    private bool didForestLevelTransitionMusic;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelManager[] levelManagers = FindObjectsOfType<LevelManager>();
        levelManager = levelManagers[0];
        foreach (LevelManager nextLevelManager in levelManagers)
            if (nextLevelManager.currentScene > levelManager.currentScene)
                levelManager = nextLevelManager;

        worldGenerator = FindObjectOfType<WorldGenerator>();

        if (levelManager.currentScene == 0)
            Play("Introduction Music");
        else if (levelManager.currentScene == 1)
            Play("BattleField Music");
        else if (levelManager.currentScene == 2)
            Play("Ballroom Music");
    }

    private void Update()
    {
        if (!didForestLevelTransitionMusic)
            if (levelManager && levelManager.currentScene == 0)
                if (worldGenerator && worldGenerator.currentScreen == 2)
                {
                    didForestLevelTransitionMusic = true;
                    Play("Forest Level Music");
                }
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
