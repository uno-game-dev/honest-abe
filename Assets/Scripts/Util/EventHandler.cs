using UnityEngine;

public class EventHandler : MonoBehaviour {

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
        ITEM_PICKUP,
        WEAPON_PICKUP,
        GAME_LOSE,
        GAME_WIN,
        JUMP,
        LAND
    }

    public static void SendEvent(Events e)
    {
        switch (e)
        {
            case Events.LIGHT_SWING:
                break;
            case Events.LIGHT_HIT:
                break;
            case Events.LIGHT_KILL:
                GlobalSettings.enemiesKilled++;
                Debug.Log(GlobalSettings.enemiesKilled);
                break;
            case Events.HEAVY_SWING:
                GlobalSettings.performingHeavyAttack = true;
                break;
            case Events.HEAVY_HIT:
                PerkManager.PerformPerkEffects(Perk.PerkCategory.AXE);
                break;
            case Events.HEAVY_KILL:
                GlobalSettings.enemiesKilled++;
                Debug.Log(GlobalSettings.enemiesKilled);
                break;
            case Events.WEAPON_THROW:
                break;
            case Events.ENEMY_THROW:
                break;
            case Events.ITEM_PICKUP:
                break;
            case Events.WEAPON_PICKUP:
                break;
            case Events.GAME_LOSE:
                break;
            case Events.GAME_WIN:
                break;
            case Events.JUMP:
                break;
            case Events.LAND:
                break;
        }
    }

}
