using UnityEngine;
using System.Collections;

public class MenuSoundPlayer
{
    private const int numberOfMenuSounds = 4;
    private static int currentMenuSound = 1;

    public static void PlayMenuSound()
    {
        SoundPlayer.Play("Menu Click " + currentMenuSound);
        currentMenuSound = (currentMenuSound - 1) % numberOfMenuSounds;
        currentMenuSound++;
    }

    public static void ResetMenuSoundCount()
    {
        currentMenuSound = 1;
    }
}
