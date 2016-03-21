using UnityEngine;

public class EventHandler : MonoBehaviour {

	private GameManager _gameManager;

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
		ROBERT_E_LEE_KILL,
		ACTIVATE_TRINKET_PERK
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
                break;
            case Events.LIGHT_KILL:
                Debug.Log("Light Kill");
				PerkManager.enemiesKilled++;
                break;
            case Events.HEAVY_SWING:
				AudioManager.instance.PlaySound("Heavy_Slash");
                Debug.Log("Heavy Swing");
                GlobalSettings.performingHeavyAttack = true;
                break;
            case Events.HEAVY_HIT:
                Debug.Log("Heavy Hit");
				AudioManager.instance.PlaySound("Stab_2");
				AudioManager.instance.PlaySound("Hit_Crack");
                PerkManager.PerformPerkEffects(Perk.PerkCategory.AXE);
                break;
            case Events.HEAVY_KILL:
                Debug.Log("Heavy Kill");
				AudioManager.instance.PlaySound("Hit_Crack");
				AudioManager.instance.PlaySound("Gore_1");
				PerkManager.enemiesKilled++;
                GlobalSettings.executionsPerformed++;
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
                if (other != null)
                    Debug.Log(other.name);
                break;
            case Events.WEAPON_PICKUP:
                Debug.Log("Weapon Pickup");
                break;
            case Events.PERK_PICKUP:
                Debug.Log("Perk Pickup");
                break;
            case Events.GAME_LOSE:
                Debug.Log("Game Lose");
				GlobalSettings.loseCondition = true;
                break;
            case Events.GAME_WIN:
                Debug.Log("Game Win");
				GlobalSettings.winCondition = true;
				PerkManager.UpdatePerkStatus(PerkManager.axe_dtVampirism_name, 1);
                break;
            case Events.JUMP:
                Debug.Log("Jump");
				AudioManager.instance.PlaySound("Jump");
                break;
            case Events.LAND:
				AudioManager.instance.PlaySound("Land");
                Debug.Log("Land");
                break;
			case Events.ROBERT_E_LEE_KILL:
				Debug.Log ("Killed Robert E. Lee");
				PerkManager.UpdatePerkStatus (PerkManager.trinket_agressionBuddy_name, 1);
				break;
        }
    }

}
