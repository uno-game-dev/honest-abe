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

    public float startSpawnPosition;

    public int spawnYPos = 0;
    public int spawnZPos = 10;

    public int propDensity = 3;
    public int decalDensity = 10;
    public int itemDensity = 1;

    private GameObject _camera;
    private List<Vector3> _occupiedPos;
    private System.Random _rnd;
    private bool _canSpawn = true;
    private float _lastXPos;
    private int _screenCount;
    private int _levelIndex;
    private int _easyWaveChance;
    private int _mediumWaveChance;
    private int _remainingEnemyDensity;

    // Use this for initialization
    void Start()
    {
        _lastXPos = startSpawnPosition;
        _camera = GameObject.Find("Main Camera");
        _rnd = new System.Random();
        _screenCount = 0;
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.currentSceneIsNew)
            _screenCount = 0;

        if (_camera.transform.position.x >= _lastXPos - startSpawnPosition && _canSpawn)
        {
            _canSpawn = false;
            _occupiedPos = new List<Vector3>();
            SpawnProps();
            // SpawnDecals(); - Disabled until art assets are ready
            SpawnItems();
            //Only counting down the boss for alpha
            SpawnBoss();
			else
			{
				SpawnItems();
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
            Instantiate(props[r], GetRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
        }
    }

    private void SpawnDecals()
    {
        for (int i = 0; i < decalDensity; i++)
        {
            int r = _rnd.Next(decals.Count);
            Instantiate(decals[r], GetRandomEmptyPos(0.5f), Quaternion.Euler(0, 0, 0));
        }
    }

    private void SpawnItems()
    {
        for (int i = 0; i < itemDensity; i++)
        {
            int r = _rnd.Next(items.Count);
            Instantiate(items[r], GetRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
        }
    }

    private void SpawnEnemies()
    {
        _remainingEnemyDensity = 0;

        switch (GetWaveDifficulty())
        {
            case 0:
                _remainingEnemyDensity = _rnd.Next(5, 8);
                break;
            case 1:
                _remainingEnemyDensity = _rnd.Next(8, 12);
                break;
            case 2:
                _remainingEnemyDensity = _rnd.Next(12, 16);
                break;
        }
        Debug.Log("Spawning wave of density " + _remainingEnemyDensity);
        while (_remainingEnemyDensity > 0)
        {
            int r = GetRandomEnemyBasedOnCurrentLevelAndDensity();
            Debug.Log("r = " + r);
            if (r == -1)
                break;
            Instantiate(enemies[r], GetRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
            Debug.Log("Enemy type " + r + " spawned, remaining density: " + _remainingEnemyDensity);
        }
	}

	private void SpawnBoss()
    {
        bool spawn = false;
        switch (_levelIndex)
        {
            case 0:
                if (_screenCount == GlobalSettings.screensInLevel1)
                    spawn = true;
                break;
            case 1:
                if (_screenCount == GlobalSettings.screensInLevel2)
                    spawn = true;
                break;
            case 2:
                if (_screenCount == GlobalSettings.screensInLevel3)
                    spawn = true;
                break;
        }
        if (spawn)
            Instantiate(bosses[_levelIndex], GetRandomEmptyPos(1f), Quaternion.Euler(0, 0, 0));
	}

    private int GetWaveDifficulty()
    {
        int r = _rnd.Next(101);
        switch (_levelIndex)
        {
            // Forest wave breakdown: 55-30-15
            case 0:
                _easyWaveChance = GlobalSettings.minRndForEasyWaveInLevel1;
                _mediumWaveChance = GlobalSettings.minRndForMediumWaveInLevel1;
                break;
            // Battlefield wave breakdown: 33-50-15
            case 1:
                _easyWaveChance = GlobalSettings.minRndForEasyWaveInLevel2;
                _mediumWaveChance = GlobalSettings.minRndForMediumWaveInLevel2;
                break;
            // Ballroom wave breakdown: 0-0-100
            case 2:
                _easyWaveChance = GlobalSettings.minRndForEasyWaveInLevel3;
                _mediumWaveChance = GlobalSettings.minRndForMediumWaveInLevel3;
                break;
        }
        if (r >= _easyWaveChance)
        {
            Debug.Log("Easy Wave Spawned");
            return 0;
        }
        else if (r >= _mediumWaveChance)
        {
            Debug.Log("Medium Wave Spawned");
            return 1;
        }
        else
        {
            Debug.Log("Hard Wave Spawned");
            return 2;
        }
    }

    private int GetRandomEnemyBasedOnCurrentLevelAndDensity()
    {
        int r = -1;
        if (_remainingEnemyDensity <= 0)
            return r;
        switch (_levelIndex)
        {
            // Ensure that spawned enemies do not reduce _remainingEnemyDensity below 0
            case 0:
                if (_remainingEnemyDensity == 1 || _screenCount <= 5)
                    r = 0;
                else
                    r = _rnd.Next(2);
                break;
            case 1:
                if (_remainingEnemyDensity <= 2)
                    r = _rnd.Next(2);
                else if(_remainingEnemyDensity <= 1)
                    r = 0;
                else
                    r = _rnd.Next(3);
                break;
            case 2:
                if (_remainingEnemyDensity <= 3)
                    r = _rnd.Next(3);
                else if (_remainingEnemyDensity <= 2)
                    r = _rnd.Next(2);
                else if(_remainingEnemyDensity <= 1)
                    r = 0;
                else
                    r = _rnd.Next(4);
                break;
        }
        _remainingEnemyDensity -= r + 1;
        return r;
    }

	private Vector3 GetRandomEmptyPos(float z)
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
