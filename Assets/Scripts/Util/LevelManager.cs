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
        Destroy(GameObject.Find("Player"));
        GameObject.Find("PerkManager").GetComponent<PerkManager>().Reset();
        SceneManager.LoadScene(0);
	}
}
