using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// The LevelManager class handles level transitions and selection.
public class LevelManager : MonoBehaviour {

	public int startingScene;
	public int currentScene;

	private static LevelManager _instance;

	// Use this for initialization
	void Awake()
	{
		if (_instance == null)
		{
			DontDestroyOnLoad(gameObject);
			_instance = this;
			currentScene = startingScene;
			SceneManager.LoadScene(currentScene);
		}
		else if (_instance != this)
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	public void loadNextLevel()
	{
		currentScene++;
		SceneManager.LoadScene(currentScene);
	}
}
