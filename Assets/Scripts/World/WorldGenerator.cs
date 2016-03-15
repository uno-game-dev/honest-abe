using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    public GameObject terrain;
    public List<GameObject> enemies;
    public List<GameObject> props;
    public List<GameObject> decals;
    public List<GameObject> items;
    public List<GameObject> bosses;

    public float startSpawnPosition = 8f;

    public int spawnYPos = 0;
    public int spawnZPos = 10;

    public int propDensity = 3;
    public int decalDensity = 10;
    public int itemDensity = 1;
    public int difficulty = 1;

    public int screensBeforeSecondEnemy = 2;
    public int screensBeforeBoss = 4;

    private GameObject _camera;
    private System.Random _rnd;
    private bool _canSpawn = true;
    private float _lastXPos;
    private List<Vector3> _occupiedPos;
    public int _screenCount;
    public int _bossIndex;

    // Use this for initialization
    void Start()
    {
        _lastXPos = startSpawnPosition;
        _camera = GameObject.Find("Main Camera");
        _rnd = new System.Random();
        _screenCount = 0;
        _bossIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {

        if (_camera.transform.position.x >= _lastXPos - startSpawnPosition && _canSpawn)
        {
            _canSpawn = false;
            _occupiedPos = new List<Vector3>();
            SpawnProps();
            // SpawnDecals(); - Disabled until art assets are ready
            if (_screenCount < screensBeforeSecondEnemy)
                SpawnEnemies(new List<GameObject>() { enemies[0] }); // Only Unarmed Enemies
            else
                SpawnEnemies(); // All Enemies
            SpawnItems();
            //Only counting down the boss for alpha
			if (_screenCount == screensBeforeBoss)
				SpawnBoss();
			else
			{
				SpawnItems();
				if (_screenCount < screensBeforeSecondEnemy)
					// Only spawn first enemy
					SpawnEnemies(new List<GameObject>() { enemies[0] });
				else
					// Spawn all enemies
					SpawnEnemies();
				_canSpawn = true;
			}
			_screenCount++;
			_lastXPos += startSpawnPosition;
		}
    }

    private void SpawnTerrain()
    {
        Instantiate(terrain, new Vector3(_lastXPos, spawnYPos, spawnZPos), Quaternion.Euler(0, 0, 0));
    }

    private void SpawnProps()
    {
        for (int i = 0; i < propDensity; i++)
        {
            int r = _rnd.Next(props.Count);
            Instantiate(props[r], getRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
        }
    }

    private void SpawnDecals()
    {
        for (int i = 0; i < decalDensity; i++)
        {
            int r = _rnd.Next(decals.Count);
            Instantiate(decals[r], getRandomEmptyPos(0.5f), Quaternion.Euler(0, 0, 0));
        }
    }

    private void SpawnItems()
    {
        for (int i = 0; i < itemDensity; i++)
        {
            int r = _rnd.Next(items.Count);
            Instantiate(items[r], getRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
        }
    }

    private void SpawnEnemies(List<GameObject> enemies = null)
    {
        int enemyDensity = 0;
        enemies = enemies == null ? this.enemies : enemies;

        switch (difficulty)
        {
            case 1:
                enemyDensity = _rnd.Next(5, 8);
                break;
            case 2:
                enemyDensity = _rnd.Next(8, 12);
                break;
            case 3:
                enemyDensity = _rnd.Next(12, 16);
                break;
        }

        for (int i = 0; i < enemyDensity; i++)
        {
            int r = _rnd.Next(enemies.Count);
            Instantiate(enemies[r], getRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
        }
	}

	private void SpawnBoss()
	{
		Instantiate(bosses[_bossIndex], getRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
	}

	private Vector3 getRandomEmptyPos(float z)
    {

        RectTransform area = (RectTransform)terrain.transform;
        double width = area.rect.width;
        double height = area.rect.height;

        float x = 0;
        float y = 0;
        bool occupied = true;

        while (occupied)
        {
            occupied = false;

            x = (float)((width * _rnd.NextDouble() * 2) - width + _lastXPos);
            y = (float)(height * _rnd.NextDouble() - height);

            foreach (Vector3 pos in _occupiedPos)
            {
                if ((Math.Abs((double)(x - pos.x)) < 1.0) && (Math.Abs((double)(y - pos.y)) < 1.0))
                {
                    occupied = true;
                    break;
                }
            }
        }

        Vector3 vector = new Vector3(x, y, z);
        _occupiedPos.Add(vector);
        return vector;
    }
}
