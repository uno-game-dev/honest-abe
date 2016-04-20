using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { HEALTH }
    public Type type;

    public void OnCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                case Type.HEALTH:
                    other.GetComponent<PlayerHealth>().Increase(GlobalSettings.healthIncreaseAmount);
                    SoundPlayer.Play("HealthPack Pickup");
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}