﻿using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
	// Levels

	public static bool winCondition = false;
	public static bool loseCondition = false;
    public static bool currentSceneIsNew = true;

    // Player

    public static int healthIncreaseAmount = 20;

    public static float gravityMultiplier = 1f;
    public static float gravity = -9.81f;

    public static float playerMoveSpeedH = 8;
    public static float playerMoveSpeedV = 6;
    public static float playerMovementSmoothing = .115f;

    public static int executionsPerformed = 0;
    public static bool performingHeavyAttack = false;

    /*
     * Perk Section
     */

    // Perk unlocked states
    public static bool axe_dtVampirism_unlocked = false;

    // Perk names
    public static string axe_none_name = "Axe_None";
    public static string axe_none_desc = "Abe's Regular Axe";

    public static string axe_dtVampirism_name = "Axe_DTVampirism";
    public static string axe_dtVampirism_desc = "Perk: Vampirism\nRestores damage threshold on all heavy attacks";


    // Section for variables that will determine unlocking perks
    public static int enemiesKilled = 0;
}
