using UnityEngine;
using System.Collections;

public class TerrainSpawner : MonoBehaviour
{
	public GameObject terrain;
	public float startSpawnPosition = 11.2f;
	public int spawnYPos = 0;
	public int terrainLength = 16;

	float lastPosition;
	GameObject cam;
	bool canSpawn = true;

	// Use this for initialization
	void Start()
	{
		lastPosition = startSpawnPosition;
		cam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (cam.transform.position.x >= lastPosition - terrainLength && canSpawn)
		{
			canSpawn = false;
			SpawnTerrain();
		}
	}

	void SpawnTerrain()
	{
		Instantiate(terrain, new Vector3(lastPosition, spawnYPos, 0), Quaternion.Euler(0, 0, 0));
		lastPosition += 11.2f;

		canSpawn = true;
	}
}
