using UnityEngine;
using UnityEngine.SceneManagement;

// The LevelManager class handles level transitions and selection.
public class LevelManager : MonoBehaviour
{
    public int startingScene;
    public int currentScene;

    private bool pauseUpdate = false;

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
        if (pauseUpdate)
            return;

        if (currentScene != SceneManager.GetActiveScene().buildIndex)
		{
			if (currentScene == 0)
				Destroy(GameObject.Find("Player"));
			else
				GameObject.Find("Player").GetComponent<Player>().Initialize();
			SceneManager.LoadScene(currentScene);
		}
	}

	public void LoadFirstLevel()
    {
        pauseUpdate = true;
        Destroy(GameObject.Find("Player"));
        GetComponent<PerkManager>().Reset();
        currentScene = 0;
        SceneManager.LoadScene(0);
        pauseUpdate = false;
    }
}
