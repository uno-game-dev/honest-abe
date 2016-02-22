using UnityEngine;

public class Perk : MonoBehaviour
{

    public enum PerkCategory
    {
        AXE,
        HAT,
        TRINKET
    }
    private PerkCategory _category;
    public PerkCategory category
    {
        get { return _category; }
    }

    public enum PerkType
    {
        AXE_FIRE,
        HAT_DTVAMPIRISM
    }
    public PerkType type;

    private string _perkName;
    public string perkName
    {
        get { return _perkName; }
    }

    // Unlocked is whether the perk is available for the player to pick up when the game starts
    // setToBeUnlocked is whether the perk was earned during the game but the player has not finished the game yet
    [HideInInspector]
    public bool unlocked, setToBeUnlocked;

    public void CheckStatus()
    {
        setToBeUnlocked = false;

        switch (type)
        {
            case PerkType.AXE_FIRE:
                _category = PerkCategory.AXE;
                _perkName = GlobalSettings.axe_fire_name;
                unlocked = GlobalSettings.axe_fire_unlocked;
                break;
            case PerkType.HAT_DTVAMPIRISM:
                _category = PerkCategory.HAT;
                _perkName = GlobalSettings.hat_dtVampirism_name;
                unlocked = GlobalSettings.hat_dtVampirism_unlocked;
                break;
        }

        if (!unlocked)
            gameObject.SetActive(false);
    }

    public void OnCollision(GameObject other)
    {
        switch (category)
        {
            case PerkCategory.AXE:
                GameObject.Find("GameManager").GetComponent<PerkManager>().activeAxePerk = this;
                break;
            case PerkCategory.HAT:
                GameObject.Find("GameManager").GetComponent<PerkManager>().activeHatPerk = this;
                break;
            case PerkCategory.TRINKET:
                GameObject.Find("GameManager").GetComponent<PerkManager>().activeTrinketPerk = this;
                break;
        }
    }
}
