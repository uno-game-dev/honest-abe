using UnityEngine;
using UnityEngine.SceneManagement;

// The LevelManager class handles level transitions and selection.
public class LevelManager : MonoBehaviour
{
    public int startingScene;
    public int currentScene;

    // Use this for initialization
    void Start()
    {
        currentScene = startingScene;
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
        SceneManager.LoadScene(currentScene);
    }
}
