using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : Health
{
    [HideInInspector]
    private GameManager _gameManager;
    [HideInInspector]
    private BossHealthSlider _bossHealthSlider;
	private WorldGenerator _worldGenerator;
    private int _totalHealth;
    private int _tempHealth;
    private int _currentTargetHealthForExecution;

	// Use this for initialization
	void Start()
	{
		_worldGenerator = GameObject.Find("Level").GetComponent<WorldGenerator>();
        _bossHealthSlider = GameObject.Find("BossHealthUI").GetComponent<BossHealthSlider>();
		_bossHealthSlider.Reset(health);
        _totalHealth = health;
        _currentTargetHealthForExecution = _totalHealth - (_totalHealth / 4);
    }

	// Update is called once per frame
	void Update()
	{
	}

	public override void Increase(int damage)
	{
		_tempHealth = health;
		_tempHealth += damage;

		if (_tempHealth >= 100)
			health = 100;
		else
			health += damage;

		_bossHealthSlider.UpdateBossHealth(health);
	}

	public override void Decrease(int damage)
	{
		_tempHealth = health;
		_tempHealth -= damage;

        if (_tempHealth <= 0)
        {
            health = 0;
            alive = false;
            if (GetComponent<Boss>().bossName == "Robert-E-Lee")
            {
                Death[] enemies = FindObjectsOfType<Death>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].gameObject.tag != "Player") enemies[i].enabled = true;
                }
                EventHandler.SendEvent(EventHandler.Events.ROBERT_E_LEE_KILL);
                EventHandler.SendEvent(EventHandler.Events.GAME_WIN);
            }
            else if (GetComponent<Boss>().bossName == "Bear")
            {
                Death[] enemies = FindObjectsOfType<Death>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].gameObject.tag != "Player") enemies[i].enabled = true;
                }
                EventHandler.SendEvent(EventHandler.Events.LEVEL_WIN, gameObject);
            }
            else if (GetComponent<Boss>().bossName == "Officer-Boss")
            {
                Death[] enemies = FindObjectsOfType<Death>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].gameObject.tag != "Player") enemies[i].enabled = true;
                }
                EventHandler.SendEvent(EventHandler.Events.LEVEL_WIN, gameObject);
            }
            else
                EventHandler.SendEvent(EventHandler.Events.LEVEL_NEXT);
            DeathSequence();
        }
        else
        {
            health -= damage;
            if (gameObject.tag != "Player" && playerAttack.attackState == Attack.State.Heavy && (health < _currentTargetHealthForExecution))
            {
                playerAttack.GetComponentInChildren<AttackArea>().AddToChainOnDeath(gameObject, true);
                EventHandler.SendEvent(EventHandler.Events.HEAVY_KILL);
            }
            if (health < _currentTargetHealthForExecution)
            {
                _currentTargetHealthForExecution -= _totalHealth / 4;
                if (gameObject.name == "Bear(Clone)")
				{
					int n = (_totalHealth - health) / (_totalHealth / 4);
					Debug.Log("Spawning " + n + " Bushwhackers");
					_worldGenerator.SpawnBushwhacker(n);
				}
				else
				if (gameObject.name == "Officer-Boss(Clone)")
					_worldGenerator.SpawnWaveDuringBoss();
			}
        }
		_bossHealthSlider.UpdateBossHealth(health);
	}
}