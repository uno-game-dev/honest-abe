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
        HEALTH
    }

    public ItemType type;

    public void OnCollision()
    {
        switch (type)
        {
            case ItemType.HEALTH:
                Debug.Log("I am health");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
