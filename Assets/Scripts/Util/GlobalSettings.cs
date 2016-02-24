using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static int healthIncreaseAmount = 20;

    public static float gravityMultiplier = 1f;

    public static float gravity = -9.81f;

    public static float playerMoveSpeedH = 8;
    public static float playerMoveSpeedV = 6;
    public static float playerMovementSmoothing = .115f;
	
	public static bool executionPerformed = false;


    /*
     * Perk Section
     */

    // Perk unlocked states
    public static bool axe_fire_unlocked = false;
    public static bool hat_dtVampirism_unlocked = false;

    // Perk names
    public static string axe_fire_name = "Axe_Fire";
    public static string hat_dtVampirism_name = "Hat_DTVampirism";
}
