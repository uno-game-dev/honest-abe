using UnityEngine;
using UnityEngine.SceneManagement;

// The LevelManager class handles level transitions and selection.
public class LevelManager : MonoBehaviour
{
    public int startingScene;
    public int currentScene;

    void Awake()
    {
        GlobalSettings.currentSceneIsNew = true;
    }

    // Use this for initialization
    void Start()
    {
        currentScene = startingScene;
        SceneManager.LoadScene(currentScene);
    }
	
	// Update is called once per frame
	void Update()
	{
        if (currentScene != SceneManager.GetActiveScene().buildIndex)
            loadCurrentLevel();
    }

    public void loadCurrentLevel()
	{
		GameObject.Find("Player").GetComponent<Player>().ResetPosition();
		SceneManager.LoadScene(currentScene);
    }

    public void loadNextLevel()
    {
        currentScene++;
		GameObject.Find("Player").GetComponent<Player>().ResetPosition();
		SceneManager.LoadScene(currentScene);
    }
}
