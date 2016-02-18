using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { HEALTH }

    public int increaseAmount;
    public Type type;

    private BaseCollision _collision;

    private void Awake()
    {
        increaseAmount = 20;
        _collision = GetComponent<BaseCollision>();
    }

    private void OnEnable()
    {
        _collision.OnCollision += OnCollision;
    }

    private void OnDisable()
    {
        _collision.OnCollision -= OnCollision;
    }

    private void Update()
    {
        _collision.Tick();
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Player")
        {
            switch (type)
            {
                case Type.HEALTH:
                    hit.collider.GetComponent<Health>().Increase(increaseAmount); // This is where you call the function that updates the player's health
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}