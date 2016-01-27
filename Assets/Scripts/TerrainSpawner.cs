using UnityEngine;

public class TerrainSpawner : MonoBehaviour {

	public GameObject terrain;
	public float startSpawnPosition = 8f;
	public int spawnYPos = 0;

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

		if (cam.transform.position.x >= lastPosition - 16 && canSpawn) {
			canSpawn = false;
			SpawnTerrain();
		}
	}

	void SpawnTerrain() {

		Instantiate(terrain, new Vector3(lastPosition, spawnYPos, 0), Quaternion.Euler(0, 0, 0));
		lastPosition += startSpawnPosition;

		canSpawn = true;
	}
}
