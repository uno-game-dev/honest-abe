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
        LAND
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
                Debug.Log("Light Swing");
                break;
            case Events.LIGHT_HIT:
                Debug.Log("Light Hit");
                break;
            case Events.LIGHT_KILL:
                Debug.Log("Light Kill");
                GlobalSettings.enemiesKilled++;
                break;
            case Events.HEAVY_SWING:
                Debug.Log("Heavy Swing");
                GlobalSettings.performingHeavyAttack = true;
                break;
            case Events.HEAVY_HIT:
                Debug.Log("Heavy Hit");
                PerkManager.PerformPerkEffects(Perk.PerkCategory.AXE);
                break;
            case Events.HEAVY_KILL:
                Debug.Log("Heavy Kill");
                GlobalSettings.enemiesKilled++;
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
                PerkManager.UpdatePerkStatus(GlobalSettings.axe_dtVampirism_name, 1);
                break;
            case Events.JUMP:
                Debug.Log("Jump");
                break;
            case Events.LAND:
                Debug.Log("Land");
                break;
        }
    }

}
