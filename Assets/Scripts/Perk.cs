using UnityEngine;

public class Perk : MonoBehaviour
{

    public enum PerkCategory
    {
        NONE_AXE,
        NONE_HAT,
        NONE_TRINKET,
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
        NONE_AXE,
        NONE_HAT,
        NONE_TRINKET,
        AXE_DTVAMPIRISM
    }
    public PerkType type;

    private string _perkName;
    public string perkName
    {
        get { return _perkName; }
    }

    private string _perkDesc;
    public string perkDesc
    {
        get { return _perkDesc; }
    }

    // Unlocked is whether the perk is available for the player to pick up when the game starts
    // setToBeUnlocked is whether the perk was earned during the game but the player has not finished the game yet
    [HideInInspector]
    public bool unlocked, setToBeUnlocked;


    /*
     * References to scripts affected by the perks
     */
    private PlayerHealth playerHealth;
    private Weapon currentPlayerWeapon;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        currentPlayerWeapon = GetComponent<Weapon>();
    }

    public void CheckStatus()
    {
        setToBeUnlocked = false;

        switch (type)
        {
            case PerkType.NONE_AXE:
                _category = PerkCategory.NONE_AXE;
                _perkName = GlobalSettings.axe_none_name;
                _perkDesc = GlobalSettings.axe_none_desc;
                unlocked = true;
                break;
            case PerkType.NONE_HAT:
                _category = PerkCategory.NONE_HAT;
                _perkName = null;
                unlocked = true;
                break;
            case PerkType.NONE_TRINKET:
                _category = PerkCategory.NONE_TRINKET;
                _perkName = null;
                unlocked = true;
                break;
            case PerkType.AXE_DTVAMPIRISM:
                _category = PerkCategory.AXE;
                _perkDesc = GlobalSettings.axe_dtVampirism_desc;
                _perkName = GlobalSettings.axe_dtVampirism_name;
                unlocked = GlobalSettings.axe_dtVampirism_unlocked;
                break;
            default:
                break;
        }

        if (!unlocked)
            gameObject.SetActive(false);
    }

    public void OnCollision(GameObject other)
    {
        switch (category)
        {
            case PerkCategory.NONE_AXE:
                PerkManager.activeAxePerk = null;
                break;
            case PerkCategory.NONE_HAT:
                PerkManager.activeHatPerk = null;
                break;
            case PerkCategory.NONE_TRINKET:
                PerkManager.activeTrinketPerk = null;
                break;
            case PerkCategory.AXE:
                PerkManager.activeAxePerk = this;
                PerkManager.AxePerkEffect += AxeEffect;
                break;
            case PerkCategory.HAT:
                PerkManager.activeHatPerk = this;
                PerkManager.HatPerkEffect += HatEffect;
                break;
            case PerkCategory.TRINKET:
                PerkManager.activeTrinketPerk = this;
                PerkManager.TrinketPerkEffect += TrinketEffect;
                break;
            default:
                break;
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void AxeEffect()
    {
        if (type == PerkType.AXE_DTVAMPIRISM)
        {
            playerHealth.IncreaseDT((int)(currentPlayerWeapon.lightDamage / 4));
        }
    }

    private void HatEffect()
    {

    }

    private void TrinketEffect()
    {

    }
}