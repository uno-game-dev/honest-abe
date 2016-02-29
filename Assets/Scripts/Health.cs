using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public int health;

    // Use this for initialization
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Increase(int amount)
    {
        health += amount;
        UpdateHealth();
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
                GlobalSettings.executionPerformed = true;
                ShowExecution();
            }
            if (gameObject.tag == "Boss")
            {
                GlobalSettings.bossFight = false;
                GameManager.win = true; // TESTING for Win game in alpha
            }
            Destroy(gameObject);
        }
        UpdateHealth();
    }

    //For testing purposes
    public virtual void UpdateHealth()
    {
    }

    public void ShowExecution()
    {
        GameObject number = new GameObject();
        number.name = "Execution";
        TextMesh tm = number.AddComponent<TextMesh>();
        tm.text = "R.I.P.";
        tm.fontSize = 24;
        tm.color = Color.red;
        tm.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        tm.transform.position = transform.position;
        FloatUpAndDestroy f = number.AddComponent<FloatUpAndDestroy>();
        f.floatGravityMultiplier = 0.5f;
        f.floatVelocity = 2;
    }

}
