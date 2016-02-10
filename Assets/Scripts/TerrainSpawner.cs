using UnityEngine;

public class TerrainSpawner : MonoBehaviour {

	public GameObject terrain;
	public GameObject prop;
	public GameObject enemy;
	public float startSpawnPosition = 8f;
	public int spawnYPos = 0;
	public int spawnZPos = 10;

	float lastPosition;
	GameObject cam;
	bool canSpawn = true;

	// Use this for initialization
	void Start() {

		lastPosition = startSpawnPosition;
		cam = GameObject.Find("Main Camera");
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

	void SpawnTerrain() {

		Instantiate(terrain, new Vector3(lastPosition, spawnYPos, spawnZPos), Quaternion.Euler(0, 0, 0));
	}

	void SpawnProp() {

		Instantiate(prop, new Vector3(lastPosition, spawnYPos + 10, 1), Quaternion.Euler(0, 0, 0));
	}

	void SpawnEnemy() {

		Instantiate(enemy, new Vector3(lastPosition, spawnYPos - 10, 1), Quaternion.Euler(0, 0, 0));
	}
}
