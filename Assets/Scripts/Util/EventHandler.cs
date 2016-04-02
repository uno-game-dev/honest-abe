using UnityEngine;

public class EventHandler : MonoBehaviour
{

    private static GameObject player;
    private static Perk bfaPerk, bearAbePerk;

    public enum Events
    {
        LIGHT_SWING,
        LIGHT_HIT,
        LIGHT_KILL,
        HEAVY_SWING,
        HEAVY_HIT,
        HEAVY_KILL,
        WEAPON_THROW,
        ENEMY_THROW,
        ENEMY_GRAB,
        ITEM_PICKUP,
        WEAPON_PICKUP,
        PERK_PICKUP,
        GAME_LOSE,
        GAME_WIN,
        JUMP,
        LAND,
        STEP,
        BEAR_HIT,
        BEAR_HIT_THROWN,
		ROBERT_E_LEE_KILL
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
                AudioManager.instance.PlaySound("Light_Slash");
                Debug.Log("Light Swing");
                break;
            case Events.LIGHT_HIT:
                AudioManager.instance.PlaySound("Stab_2");
                AudioManager.instance.PlaySound("Impact");
                Debug.Log("Light Hit");

                // If our player hits an enemy with no weapon, he loses the BFA perk unlock
                if (player == null) player = GameObject.Find("Player");
                if (player.GetComponent<Attack>().emptyHanded)
                {
                    if (bfaPerk == null) bfaPerk = PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.axe_bfa_name));
                    bfaPerk.setToBeUnlocked = false;
                }
                break;
            case Events.LIGHT_KILL:
                Debug.Log("Light Kill");
                PerkManager.enemiesKilled++;
                break;
            case Events.HEAVY_SWING:
                AudioManager.instance.PlaySound("Heavy_Slash");
                Debug.Log("Heavy Swing");
                break;
            case Events.HEAVY_HIT:
                Debug.Log("Heavy Hit");
                AudioManager.instance.PlaySound("Stab_2");
                AudioManager.instance.PlaySound("Hit_Crack");
                PerkManager.PerformPerkEffects(Perk.PerkCategory.AXE);

                // If our player hits an enemy with no weapon, he loses the BFA perk unlock
                if (player == null) player = GameObject.Find("Player");
                if (player.GetComponent<Attack>().emptyHanded)
                {
                    if (bfaPerk == null) bfaPerk = PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.axe_bfa_name));
                    bfaPerk.setToBeUnlocked = false;
                }
                break;
            case Events.HEAVY_KILL:
                Debug.Log("Heavy Kill");
                AudioManager.instance.PlaySound("Hit_Crack");
                AudioManager.instance.PlaySound("Gore_1");
				PerkManager.enemiesKilled++;
                GameObject.Find("Player").GetComponent<PlayerHealth>().executionsPerformed++;
                break;
            case Events.WEAPON_THROW:
                Debug.Log("Weapon Throw");
                break;
            case Events.ENEMY_GRAB:
                Debug.Log("Enemy Grab");
                break;
            case Events.ENEMY_THROW:
                Debug.Log("Enemy Throw");
                break;
            case Events.ITEM_PICKUP:
                Debug.Log("Item Pickup");
				if(other != null && other.name == "HealthKit") {
					// If we pickup a health pickup then it cancels out the ability to get the mary todd's lockette perk
					PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.trinket_maryToddsLockette_name)).setToBeUnlocked = false;
                    Debug.Log(PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.trinket_maryToddsLockette_name)).setToBeUnlocked);
                }
                break;
            case Events.WEAPON_PICKUP:
                Debug.Log("Weapon Pickup");

                // If the player picks up any weapons other than the axe, cancel out the BFA perk unlock
                // The axe is considered a perk, so will not trigger a WEAPON_PICKUP event
                if (bfaPerk == null) bfaPerk = PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.axe_bfa_name));
                bfaPerk.setToBeUnlocked = false;
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
            case Events.GAME_LOSE:
                Debug.Log("Game Lose");
                GameObject.Find("UI").GetComponent<UIManager>().ActivateLoseUI();
                break;
            case Events.GAME_WIN:
                Debug.Log("Game Win");
                PerkManager.UpdatePerkStatus(PerkManager.axe_dtVampirism_name, 1);
                GameObject.Find("GameManager").GetComponent<GameManager>().Win();
                break;
            case Events.JUMP:
                Debug.Log("Jump");
                AudioManager.instance.PlaySound("Jump");
                break;
            case Events.LAND:
                AudioManager.instance.PlaySound("Land");
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
                        if (bearAbePerk == null) bearAbePerk = PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.hat_bearHands_name));
                        bearAbePerk.setToBeUnlocked = false;
                    }
                }
                break;
            case Events.BEAR_HIT_THROWN:
                if (bearAbePerk == null) bearAbePerk = PerkManager.perkList.Find(x => x.perkName.Equals(PerkManager.hat_bearHands_name));
                bearAbePerk.setToBeUnlocked = false;
                break;
			case Events.ROBERT_E_LEE_KILL:
				Debug.Log ("Killed Robert E. Lee");
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health >= 80)
                    PerkManager.UpdatePerkStatus (PerkManager.trinket_agressionBuddy_name, 1);
				break;
			case Events.STEP:
                AudioManager.instance.PlayFootstep();
                Debug.Log("Step");
                break;
        }
    }

}