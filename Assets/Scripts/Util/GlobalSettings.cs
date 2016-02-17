using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static int healthIncreaseAmount = 20;

    public static float gravityMultiplier = 1f;

    public static float gravity = -9.81f;

    public static float playerMoveSpeedH = 8;
    public static float playerMoveSpeedV = 6;
    public static float playerMovementSmoothing = .115f;


    // Perks and their unlocked state
    public static bool axe_fire_unlocked = false;
    public static bool hat_dtVampirism_unlocked = false;
}
