using UnityEngine;
using System;
using System.Collections.Generic;

public class TerrainSpawner : MonoBehaviour {

	public GameObject terrain;
	public List<GameObject> enemies;
	public List<GameObject> props;
	public float startSpawnPosition = 8f;
	public int spawnYPos = 0;
	public int spawnZPos = 10;
	public int propDensity = 3;
	public int difficulty = 1;

	private GameObject cam;
	private System.Random rnd;
	private bool canSpawn = true;
	private float lastPosition;
	private List<Vector3> occupiedPositions;

	// Use this for initialization
	void Start()
	{
		lastPosition = startSpawnPosition;
		cam = GameObject.Find("Main Camera");
		rnd = new System.Random();
	}
	
	// Update is called once per frame
	void Update() {

		if (cam.transform.position.x >= lastPosition - startSpawnPosition && canSpawn) {
			canSpawn = false;
			SpawnTerrain();
			occupiedPositions = new List<Vector3>();
			SpawnProp();
			SpawnEnemy();
			lastPosition += startSpawnPosition;
			canSpawn = true;
		}
	}

	private void SpawnTerrain() {

		Instantiate(terrain, new Vector3(lastPosition, spawnYPos, spawnZPos), Quaternion.Euler(0, 0, 0));
	}

	private void SpawnProp() {

		for (int i = 0; i < propDensity; i++)
		{
			int r = rnd.Next(props.Count);
			Instantiate(props[r], getRandomPos(), Quaternion.Euler(0, 0, 0));
		}
	}

	private void SpawnEnemy() {

		int enemyDensity = 0;
		
		switch (difficulty) {
			case 1:
				enemyDensity = rnd.Next(5, 8);
				break;
			case 2:
				enemyDensity = rnd.Next(8, 12);
				break;
			case 3:
				enemyDensity = rnd.Next(12, 16);
				break;
		}
		
		for (int i = 0; i < enemyDensity; i++) {
			int r = rnd.Next(enemies.Count);
			Instantiate(enemies[r], getRandomPos(), Quaternion.Euler(0, 0, 0));
		}
	}

	private Vector3 getRandomPos() {

		RectTransform area = (RectTransform)terrain.transform;
		double width = area.rect.width;
		double height = area.rect.height * 0.5;

		float x = 0;
		float y = 0;
		bool occupied = true;

		while (occupied) {
			occupied = false;

			x = (float)((width * rnd.NextDouble() * 2) - width + lastPosition);
			y = (float)(height * rnd.NextDouble() - height);

			foreach (Vector3 pos in occupiedPositions) {
				if ((Math.Abs((double)(x - pos.x)) < 1.0) && (Math.Abs((double)(y - pos.y)) < 1.0)) {
					occupied = true;
					Debug.Log("OVERLAP!");
					break;
				}
			}
		}

		Vector3 vector = new Vector3(x, y, 1);
		occupiedPositions.Add(vector);
		return vector;
	}
}
