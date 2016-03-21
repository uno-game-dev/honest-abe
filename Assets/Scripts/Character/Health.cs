using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int additionalHealthFloor;
    public int additionalHealthCeiling;
	public bool alive;
	
	private Boss _boss;
    private System.Random _rnd;
    private Attack playerAttack;

    void Awake()
    {
        _rnd = new System.Random();
        health += _rnd.Next(additionalHealthFloor, additionalHealthCeiling + 1);
		alive = true;

        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }

    public void RandomizeHealth()
    {
    }

    public virtual void Increase(int amount)
    {
        health += amount;
    }

    public virtual void Decrease(int damage)
    {
        health -= damage;
        //If the hit would kill the gameObject
        if (health <= 0)
        {
            health = 0;
			alive = false;
			// Execution Check
            if (gameObject.tag == "Enemy")
			{
				if (gameObject.tag != "Player" && GameObject.Find("Player").GetComponent<Attack>().attackState == Attack.State.Heavy)
				{
					ShowExecution();
					EventHandler.SendEvent(EventHandler.Events.HEAVY_KILL);
				}
				else if (playerAttack.attackState == Attack.State.Light)
                    EventHandler.SendEvent(EventHandler.Events.LIGHT_KILL);
            }
            Destroy(gameObject);
        }
    }

    public void ShowExecution()
    {
        GameObject number = new GameObject();
        number.name = "Execution";
        TextMesh tm = number.AddComponent<TextMesh>();
        tm.text = "RIP";
        tm.fontSize = 24;
        tm.color = Color.red;
        tm.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        tm.transform.position = transform.position;
        FloatUpAndDestroy f = number.AddComponent<FloatUpAndDestroy>();
        f.floatGravityMultiplier = 0.5f;
        f.floatVelocity = 2;
    }
}
