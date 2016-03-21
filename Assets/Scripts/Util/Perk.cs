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
        AXE_DTVAMPIRISM,
		TRINKET_AGGRESSIONBUDDY	
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
    private Weapon weapon;
    private Attack playerAttack;
	[HideInInspector]
	public static float trinketTimeStamp = 0f;
	private static float trinketCoolDown;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        weapon = null;
        playerAttack = null;
    }

    public void CheckStatus()
    {
        setToBeUnlocked = false;

        switch (type)
        {
            case PerkType.NONE_AXE:
                _category = PerkCategory.NONE_AXE;
				_perkName = PerkManager.axe_none_name;
				_perkDesc = PerkManager.axe_none_desc;
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
				_perkDesc = PerkManager.axe_dtVampirism_desc;
				_perkName = PerkManager.axe_dtVampirism_name;
				unlocked = PerkManager.axe_dtVampirism_unlocked;
                break;
            default:
                break;
			case PerkType.TRINKET_AGGRESSIONBUDDY:
				_category = PerkCategory.TRINKET;
				_perkDesc = PerkManager.trinket_agressionBuddy_desc;
				_perkName = PerkManager.trinket_agressionBuddy_name;
				unlocked = PerkManager.trinket_agressionBuddy_unlocked;
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
                playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
                weapon = GetComponent<Weapon>();
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
				trinketCoolDown = 30f;
				if (GameObject.Find ("Trinket_Sprite_Placeholder") != null) {
					GameObject.Find ("Trinket_Sprite_Placeholder").SetActive (false);
				}
				break;
            default:
                break;
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void AxeEffect()
    {
        if (type == PerkType.AXE_DTVAMPIRISM && playerAttack.attackState == Attack.State.Heavy)
        {
            playerHealth.IncreaseDT((int)(weapon.lightDamage / 2));
        }
    }

    private void HatEffect()
    {

    }

    private void TrinketEffect()
    {
		if(type == PerkType.TRINKET_AGGRESSIONBUDDY){
			//Need to ask design team how much to increase DT
			//Acitvate Effect after 30second cooldown
			if (trinketTimeStamp <= Time.time) {
				playerHealth.IncreaseDT(10);
				trinketTimeStamp = Time.time + trinketCoolDown;
			}
		}
    }
}