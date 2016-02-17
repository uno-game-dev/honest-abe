using UnityEngine;

public class Perk : MonoBehaviour
{

    public enum PerkType
    {
        AXE_FIRE,
        HAT_DTVAMPIRISM
    }
    public PerkType type;

    private bool perkUnlocked;

    public void CheckStatus()
    {
        switch (type)
        {
            case PerkType.AXE_FIRE:
                perkUnlocked = GlobalSettings.axe_fire_unlocked;
                break;
            case PerkType.HAT_DTVAMPIRISM:
                perkUnlocked = GlobalSettings.hat_dtVampirism_unlocked;
                break;
        }

        if (!perkUnlocked)
            gameObject.SetActive(false);
    }

    public void OnCollision(GameObject other)
    {

    }

}
