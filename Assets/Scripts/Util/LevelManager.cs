using UnityEngine;
using UnityEngine.SceneManagement;

// The LevelManager class handles level transitions and selection.
public class LevelManager : MonoBehaviour
{
    public int startingScene;
    public int currentScene;
    public bool currentSceneIsNew;

    // Use this for initialization
    void Start()
    {
        currentScene = startingScene;
        currentSceneIsNew = true;
        SceneManager.LoadScene(currentScene);
    }
	
	// Update is called once per frame
	void Update()
	{
    }

    public void loadCurrentLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void loadNextLevel()
    {
        currentScene++;
        currentSceneIsNew = true;
        SceneManager.LoadScene(currentScene);
    }
}
