using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    // Levels
    public static bool currentSceneIsNew = true;

    public static int screensInLevel1 = 10;
    public static int screensInLevel2 = 10;
    public static int screensInLevel3 = 10;

    // Player

    public static int healthIncreaseAmount = 20;

    public static float gravityMultiplier = 1f;
    public static float gravity = -9.81f;

    public static float playerMoveSpeedH = 8;
    public static float playerMoveSpeedV = 6;
    public static float playerMovementSmoothing = .115f;
    public static float playerDefaultUnarmedLightDamage = 2;
    public static float playerDefaultUnarmedHeavyDamage = 3;

    // Enemy Waves

    public static int minRndForEasyWaveInLevel1 = 45;
    public static int minRndForMediumWaveInLevel1 = 15;
    public static int minRndForEasyWaveInLevel2 = 65;
    public static int minRndForMediumWaveInLevel2 = 15;
    public static int minRndForEasyWaveInLevel3 = 101;
    public static int minRndForMediumWaveInLevel3 = 101;
}