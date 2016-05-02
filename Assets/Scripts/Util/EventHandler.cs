using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public string eventToExecute;

    private static GameObject player;
    private static Perk bfaPerk, bearAbePerk, sluggerPerk;
    private static int amountOfWeaponPickUp = 0;
    private static int goalToUnlockSFPerk = 20;

    private string _eventString;

    public enum Events
    {
        LIGHT_SWING,
        LIGHT_HIT,
        LIGHT_KILL,
        HEAVY_SWING,
        HEAVY_HIT,
        HEAVY_KILL,
        WEAPON_THROW,
        WEAPON_THROW_KILL,
        GUN_FIRE_KILL,
        ENEMY_THROW,
        ENEMY_GRAB,
        ITEM_PICKUP,
        WEAPON_PICKUP,
        PERK_PICKUP,
		LEVEL_WIN,
        LEVEL_NEXT,
		GAME_LOSE,
        GAME_WIN,
        JUMP,
        LAND,
        STEP,
        BEAR_HIT,
        BEAR_HIT_THROWN,
        ROBERT_E_LEE_KILL,
        STEAL_ENEMY_WEAPON,
        ENEMY_CLOSE_TO_STEAL_WEAPON,
        BUTTON_CLICK
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(eventToExecute))
        {
            _eventString = eventToExecute;
            eventToExecute = null;
            try
            {
                Events eventValue = (Events)Enum.Parse(typeof(Events), _eventString);
                if (Enum.IsDefined(typeof(Events), eventValue) | eventValue.ToString().Contains(","))
                {
                    Debug.Log("Converted '" + _eventString + "' to " + eventValue.ToString() + ".");
                    SendEvent(eventValue, null);
                }
                else
                    Debug.Log("'" + _eventString + "' is not an underlying value of the Events enumeration.");
            }
            catch (ArgumentException)
            {
                Debug.Log("'" + _eventString + "' is not a member of the Events enumeration.");
            }
        }
    }

    public static void SendEvent(Events e)
    {
        SendEvent(e, null);
    }

    public static void SendEvent(Events e, GameObject other)
    {
        switch (e)
        {
            case Events.LIGHT_SWING:
                //AudioManager.instance.PlaySound("Light_Slash");
                Debug.Log("Light Swing");
                break;
            case Events.LIGHT_HIT:
                //AudioManager.instance.PlaySound("Stab_2");
                //AudioManager.instance.PlaySound("Impact");
                Debug.Log("Light Hit");

                // If our player hits an enemy with no weapon, he loses the BFA perk unlock
                if (player == null) player = GameObject.Find("Player");
                if (player.GetComponent<Attack>().emptyHanded)
                {
                    PerkManager.axe_bfa_to_be_unlocked = false;
                }
                break;
            case Events.LIGHT_KILL:
                Debug.Log("Light Kill");
                PerkManager.axe_slugger_to_be_unlocked = false;
                PerkManager.enemiesKilled++;
                break;
            case Events.HEAVY_SWING:
                //AudioManager.instance.PlaySound("Heavy_Slash");
                Debug.Log("Heavy Swing");
                break;
            case Events.HEAVY_HIT:
                Debug.Log("Heavy Hit");
                //AudioManager.instance.PlaySound("Stab_2");
                //AudioManager.instance.PlaySound("Hit_Crack");
                PerkManager.PerformPerkEffects(Perk.PerkCategory.AXE);

                // If our player hits an enemy with no weapon, he loses the BFA perk unlock
                if (player == null)
                    player = GameObject.Find("Player");
                if (player.GetComponent<Attack>().emptyHanded)
                {
                    PerkManager.axe_bfa_to_be_unlocked = false;
                }
                break;
            case Events.HEAVY_KILL:
                Debug.Log("Heavy Kill");
                PerkManager.enemiesKilled++;
                GameObject.Find("Player").GetComponent<PlayerHealth>().executionsPerformed++;
                break;
            case Events.WEAPON_THROW:
                Debug.Log("Weapon Throw");
                if ((other != null) && (PerkManager.activeHatPerk != null) && (PerkManager.activeHatPerk.perkName == "Hat_StickyFingers"))
                {
                    other.GetComponent<BoxCollider2D>().size = new Vector2(6.0f, 1.0f);
                    other.GetComponent<BoxCollider2D>().offset = new Vector2(0.75f, 0.5f);
                }
                break;
            case Events.WEAPON_THROW_KILL:
                Debug.Log("Weapon Throw Kill");
                PerkManager.axe_slugger_to_be_unlocked = false;
                break;
            case Events.GUN_FIRE_KILL:
                Debug.Log("Gun Fire Kill");
                PerkManager.axe_slugger_to_be_unlocked = false;
                break;
            case Events.ENEMY_GRAB:
                Debug.Log("Enemy Grab");
                break;
            case Events.ENEMY_THROW:
                Debug.Log("Enemy Throw");
                break;
            case Events.ITEM_PICKUP:
                Debug.Log("Item Pickup");
                if (other != null && other.GetComponent<Item>() != null && other.GetComponent<Item>().type == Item.Type.HEALTH)
                {
                    // If we pickup a health pickup then it cancels out the ability to get the mary todd's lockette perk
                    PerkManager.trinket_maryToddsLockette_to_be_unlocked = false;
                }
                break;
            case Events.WEAPON_PICKUP:
                Debug.Log("Weapon Pickup");
                //If the player picks up a certain amount of weapons then the Sticky Fingers perk will be unlocked
                amountOfWeaponPickUp++;
                if (amountOfWeaponPickUp == goalToUnlockSFPerk)
                {
                    PerkManager.UpdatePerkStatus(PerkManager.hat_stickyFingers_name, 1);
                    Debug.Log("Sticky Fingers perk is unlocked");
                }

                // If the player picks up any weapons other than the axe, cancel out the BFA perk unlock
                // The axe is considered a perk, so will not trigger a WEAPON_PICKUP event
                PerkManager.axe_bfa_to_be_unlocked = false;
                break;
            case Events.PERK_PICKUP:
                Debug.Log("Perk Pickup");

                if (other != null && other.GetComponent<Perk>() != null)
                {
                    // Activate the perk
                    // If the perk is not one that needs to be activated,
                    //     then this will have no effect
                    other.GetComponent<Perk>().Activate();
                }
                break;
			case Events.LEVEL_WIN:
				Debug.Log ("Level Win");
				Perk.trinketTimeStamp = Time.time;
				Perk.performMaryToddsTimeStamp = Time.time;
				if (other != null && other.GetComponent<Boss>() != null && other.GetComponent<Boss>().bossName == "Officer-Boss")
                    GameObject.Find("UI").GetComponent<UIManager>().cutsceneManager.ChangeCutscene(CutsceneManager.Cutscenes.MID);
				break;
            case Events.LEVEL_NEXT:
                GameObject.Find("GameManager").GetComponent<LevelManager>().currentScene++;
                break;
			case Events.GAME_WIN:
                Debug.Log("Game Win");
                PerkManager.UpdatePerkStatus(PerkManager.axe_dtVampirism_name, 1);
                GameObject.Find("GameManager").GetComponent<GameManager>().Win();
                GameObject.Find("UI").GetComponent<UIManager>().cutsceneManager.ChangeCutscene(CutsceneManager.Cutscenes.END);
                break;
			case Events.GAME_LOSE:
				Debug.Log("Game Lose");
				Perk.trinketTimeStamp = Time.time;
				Perk.performMaryToddsTimeStamp = Time.time;
				if (player == null)
					player = GameObject.Find("Player");
				player.GetComponent<PlayerMotor>().enabled = false;
				player.GetComponent<PlayerControls>().enabled = false;
				GameObject.Find("UI").GetComponent<UIManager>().ActivateLoseUI();
				break;
			case Events.JUMP:
                Debug.Log("Jump");
                //AudioManager.instance.PlaySound("Jump");
                break;
            case Events.LAND:
                //AudioManager.instance.PlaySound("Land");
                Debug.Log("Land");
                break;
            case Events.BEAR_HIT:
                Debug.Log("BEAR HIT");
                // When we hit the bear, check to see if we have an Attack component (other == Player G.O.)
                if (other != null && other.GetComponent<Attack>() != null)
                {
                    // Every time we hit the bear, see if we're hitting him empty-handed
                    if (!other.GetComponent<Attack>().emptyHanded)
                    {
                        // If we hit him with a weapon, cancel out the ability to earn the perk
                        PerkManager.hat_bearHands_to_be_unlocked = false;
                    }
                }
                break;
            case Events.BEAR_HIT_THROWN:
                PerkManager.hat_bearHands_to_be_unlocked = false;
                break;
            case Events.ROBERT_E_LEE_KILL:
                Debug.Log("Killed Robert E. Lee");
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health >= 80)
                    PerkManager.UpdatePerkStatus(PerkManager.trinket_agressionBuddy_name, 1);
                break;
            case Events.STEP:
                //AudioManager.instance.PlayFootstep();
                Debug.Log("Step");
                break;
            case Events.ENEMY_CLOSE_TO_STEAL_WEAPON:
                if ((other != null) && (PerkManager.activeHatPerk != null) && (PerkManager.activeHatPerk.perkName == "Hat_StickyFingers"))
                {
                    Debug.Log("Enemy is enough to steal weapon");
                    BoxCollider2D gunBoxCollider = other.transform.FindContainsInChildren("Musket").GetComponent<BoxCollider2D>();

                    if (gunBoxCollider != null)
                    {
                        gunBoxCollider.size = new Vector2(25.0f, 20.0f);
                        gunBoxCollider.offset = new Vector2(3.5f, 2.0f);
                    }
                }
                break;
            case Events.BUTTON_CLICK:
                Debug.Log("clicked a menu button");
                MenuSoundPlayer.PlayMenuSound();
                break;
        }
    }

}