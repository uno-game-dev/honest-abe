﻿	using UnityEngine;

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
        AXE_BFA,
        HAT_BEARHANDS,
		TRINKET_AGGRESSIONBUDDY, 
		TRINKET_MARY_TODDS_LOCKETTE,
		HAT_STICKYFINGERS
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

    // unlocked is whether the perk is available for the player to pick up when the game starts
    // setToBeUnlocked is whether the perk was earned during the game but the player has not finished the game yet
    [HideInInspector]
    public bool unlocked, setToBeUnlocked, alreadyActive;

    /*
     * References to scripts affected by the perks
     */
    private PlayerHealth playerHealth;
    private Weapon weapon;
    private Attack playerAttack;
	[HideInInspector]
	public static float trinketTimeStamp = 0f;
	private static float trinketCoolDown;
	[HideInInspector]
	public static float performMaryToddsTimeStamp = 0f;
	[HideInInspector]
	public static bool maryToddsLocketteIsActive = false;
	private static float performMaryToddsCoolDown;

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
				_perkName = PerkManager.hat_none_name;
				_perkDesc = PerkManager.hat_none_desc;
                unlocked = true;
                break;
            case PerkType.NONE_TRINKET:
                _category = PerkCategory.NONE_TRINKET;
				_perkName = PerkManager.trinket_none_name;
				_perkDesc = PerkManager.trinket_none_desc;
                unlocked = true;
                break;
            case PerkType.AXE_DTVAMPIRISM:
                _category = PerkCategory.AXE;
                _perkDesc = PerkManager.axe_dtVampirism_desc;
                _perkName = PerkManager.axe_dtVampirism_name;
                unlocked = PerkManager.axe_dtVampirism_unlocked;
                break;
            case PerkType.AXE_BFA:
                _category = PerkCategory.AXE;
                _perkDesc = PerkManager.axe_bfa_desc;
                _perkName = PerkManager.axe_bfa_name;
                unlocked = PerkManager.axe_bfa_unlocked;
                if (!unlocked) setToBeUnlocked = true;
                break;
            case PerkType.HAT_BEARHANDS:
                _category = PerkCategory.HAT;
                _perkDesc = PerkManager.hat_bearHands_desc;
                _perkName = PerkManager.hat_bearHands_name;
                unlocked = PerkManager.hat_bearHands_unlocked;
                if (!unlocked) setToBeUnlocked = true;
                break;
			case PerkType.TRINKET_AGGRESSIONBUDDY:
				_category = PerkCategory.TRINKET;
				_perkDesc = PerkManager.trinket_agressionBuddy_desc;
				_perkName = PerkManager.trinket_agressionBuddy_name;
				unlocked = PerkManager.trinket_agressionBuddy_unlocked;
                break;
			case PerkType.TRINKET_MARY_TODDS_LOCKETTE:
				_category = PerkCategory.TRINKET;
				_perkDesc = PerkManager.trinket_maryToddsLockette_desc;
				_perkName = PerkManager.trinket_maryToddsLockette_name;
				unlocked = PerkManager.trinket_maryToddsLockette_unlocked;
				if (!unlocked) setToBeUnlocked = true;
                break;
			case PerkType.HAT_STICKYFINGERS:
				_category = PerkCategory.HAT;
				_perkDesc = PerkManager.hat_stickyFingers_desc;
				_perkName = PerkManager.hat_stickyFingers_name;
				unlocked = PerkManager.hat_stickyFingers_unlocked;
				break;
			default:
                break;
        }

        if (!unlocked)
            gameObject.SetActive(false);

        alreadyActive = false;
    }

    public void OnCollision(GameObject other)
    {
        switch (category)
        {
            case PerkCategory.NONE_AXE:
                PerkManager.activeAxePerk = null;
				PerkManager.UnlockCameraAfterPerkPickUp ();
				PerkManager.axePerkChosen = true;
                break;
            case PerkCategory.NONE_HAT:
                PerkManager.activeHatPerk = null;
				if (GameObject.Find ("Hat_Default") != null) {
                    GameObject.Find ("Hat_Default").transform.SetParent (GameObject.Find ("Player").transform, true);
					GameObject.Find ("Hat_Default").SetActive (false);
                    GameObject.Find("Player").GetComponent<PickupHat>().SetHat(PickupHat.HatType.Regular);
                }
				PerkManager.UnlockCameraAfterPerkPickUp ();
				PerkManager.hatPerkChosen = true;
                break;
            case PerkCategory.NONE_TRINKET:
                PerkManager.activeTrinketPerk = null;
				if (GameObject.Find ("Trinket_Default") != null) {	
					GameObject.Find ("Trinket_Default").transform.SetParent (GameObject.Find ("Player").transform, true);
					GameObject.Find ("Trinket_Default").SetActive (false);
				}
				PerkManager.UnlockCameraAfterPerkPickUp ();
				PerkManager.trinketPerkChosen = true;
                break;
            case PerkCategory.AXE:
                playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
                weapon = playerAttack.weapon;
                PerkManager.activeAxePerk = this;
                PerkManager.AxePerkEffect += AxeEffect;
				PerkManager.UnlockCameraAfterPerkPickUp();
				PerkManager.axePerkChosen = true;
                break;
			case PerkCategory.HAT:
				PerkManager.activeHatPerk = this;
				PerkManager.HatPerkEffect += HatEffect;
				if (this.type == PerkType.HAT_BEARHANDS) {
					if (GameObject.Find ("Hat_BA") != null) {
						GameObject.Find ("Hat_BA").transform.SetParent (GameObject.Find ("Player").transform, true);
						GameObject.Find ("Hat_BA").SetActive (false);
                        GameObject.Find("Player").GetComponent<PickupHat>().SetHat(PickupHat.HatType.Bear);
                    }
				}
				if (this.type == PerkType.HAT_STICKYFINGERS) {
					if (GameObject.Find ("Hat_SF") != null) {
						GameObject.Find ("Hat_SF").transform.SetParent (GameObject.Find ("Player").transform, true);
						GameObject.Find ("Hat_SF").SetActive (false);
                        GameObject.Find("Player").GetComponent<PickupHat>().SetHat(PickupHat.HatType.StickyFingers);
                    }
				}
				PerkManager.UnlockCameraAfterPerkPickUp();
				PerkManager.hatPerkChosen = true;
                break;
			case PerkCategory.TRINKET:
				PerkManager.activeTrinketPerk = this;
				PerkManager.TrinketPerkEffect += TrinketEffect;

                if (this.type == PerkType.TRINKET_AGGRESSIONBUDDY)
                {
                    if (GameObject.Find("AB_Sprite_Placeholder") != null)
                    {
                        GameObject.Find("AB_Sprite_Placeholder").transform.SetParent(GameObject.Find("Player").transform, true);
                        GameObject.Find("AB_Sprite_Placeholder").SetActive(false);
                    }
                }

                if (this.type == PerkType.TRINKET_MARY_TODDS_LOCKETTE)
                {
                    if (GameObject.Find("MT_Sprite_Placeholder") != null)
                    {
                        GameObject.Find("MT_Sprite_Placeholder").transform.SetParent(GameObject.Find("Player").transform, true);
                        GameObject.Find("MT_Sprite_Placeholder").SetActive(false);
                    }
                }
				PerkManager.UnlockCameraAfterPerkPickUp();
				PerkManager.trinketPerkChosen = true;
                break;
            default:
                break;
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }

    /*
     * Active() is used for perks that add something to to player once picked up
     *     and do not have to be constantly triggered
     *     e.g. the Bear Abe perk, where his unarmed damage is increased once picked up
     */
    public void Activate()
    {
        if (!alreadyActive)
        {
            if (type == PerkType.HAT_BEARHANDS)
            {
                GameObject.Find("Player").GetComponent<Weapon>().lightDamage *= 3;
                GameObject.Find("Player").GetComponent<Weapon>().heavyDamage *= 3;
            }

            if (type == PerkType.AXE_BFA)
            {
                GameObject.Find("Axe_BFA").GetComponent<Weapon>().lightDamage *= 2;
                GameObject.Find("Axe_BFA").GetComponent<Weapon>().heavyDamage *= 2;
                GameObject.Find("Axe_BFA").GetComponent<Weapon>().attackSize *= 2;
            }

            alreadyActive = true;
        }
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
			trinketCoolDown = 30f;
			//Acitvate Effect after 30second cooldown
			if (trinketTimeStamp <= Time.time) {
				playerHealth.IncreaseDT(20);
				trinketTimeStamp = Time.time + trinketCoolDown;
			}
		}
		if(type == PerkType.TRINKET_MARY_TODDS_LOCKETTE){
			trinketCoolDown = 120f;
			performMaryToddsCoolDown = 10f;
			if (trinketTimeStamp <= Time.time) {
				//Provide invincibility 
				//In Player Health it is checked if performMaryToddsTimeStamp >= Time.time because the invincibility only last for a certain amount of time
				performMaryToddsTimeStamp = Time.time + performMaryToddsCoolDown;
				trinketTimeStamp = Time.time + trinketCoolDown;
			}
		}
    }
}