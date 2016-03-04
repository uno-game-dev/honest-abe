using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int additionalHealthFloor;
    public int additionalHealthCeiling;

    private System.Random _rnd;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        _rnd = new System.Random();
        health += _rnd.Next(additionalHealthFloor, additionalHealthCeiling + 1);
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
            // Execution Check
            if (gameObject.tag != "Player" && GlobalSettings.performingHeavyAttack)
            {
                GlobalSettings.executionsPerformed++;
                ShowExecution();
            }
            if (gameObject.tag == "Boss")
            {
                GlobalSettings.bossFight = false;
                GameManager.win = true; // TESTING for Win game in alpha
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
