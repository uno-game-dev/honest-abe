using UnityEngine;
using System.Collections.Generic;

public class TerrainSpawner : MonoBehaviour {

	public GameObject terrain;
	public List<GameObject> enemies;
	public List<GameObject> props;
	public float startSpawnPosition = 8f;
	public int spawnYPos = 0;
	public int spawnZPos = 10;

	private GameObject cam;
	private GameObject gm;
	private GameObject lastTerrain;
	private System.Random rnd;
	private bool canSpawn = true;
	private float lastPosition;

	// Use this for initialization
	void Start() {
		rnd = new System.Random();
		lastPosition = startSpawnPosition;
		cam = GameObject.Find("Main Camera");
		gm = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update() {

		if (cam.transform.position.x >= lastPosition - startSpawnPosition && canSpawn) {
			canSpawn = false;
			SpawnTerrain();
			SpawnProp();
			SpawnEnemy();
			lastPosition += startSpawnPosition;
			canSpawn = true;
		}
	}

	private void SpawnTerrain() {

		lastTerrain = Instantiate(terrain, new Vector3(lastPosition, spawnYPos, spawnZPos), Quaternion.Euler(0, 0, 0)) as GameObject;
	}

	private void SpawnProp() {

		int r = rnd.Next(props.Count);
		Instantiate(props[r], getRandomPos(), Quaternion.Euler(0, 0, 0));
	}

	private void SpawnEnemy() {

		int r = rnd.Next(enemies.Count);
		Instantiate(enemies[r], getRandomPos(), Quaternion.Euler(0, 0, 0));
	}

	private Vector3 getRandomPos() {
		RectTransform area = (RectTransform)terrain.transform;
		double width = area.rect.width;
		double height = area.rect.height;

		float x = (float)((width * rnd.NextDouble()) - (width / 2) + lastPosition);
		float y = (float)((height * rnd.NextDouble()) - (height / 2));

		return new Vector3(x, y, 1);
	}
}
