using UnityEngine;

public class Item : MonoBehaviour
{

    public int increaseAmount;

    void Start()
    {
        increaseAmount = 20;
    }

    public enum ItemType
    {
        HEALTH,
        AXE
    }

    public ItemType type;

    public void OnCollision(GameObject other)
    {
        switch (type)
        {
            case ItemType.HEALTH:
                //other.GetComponent<Health>().Increase(increaseAmount); // This is where you call the function that updates the player's health
                Destroy(gameObject);
                break;
            case ItemType.AXE:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
