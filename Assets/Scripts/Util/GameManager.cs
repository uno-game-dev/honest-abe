using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool perkChosen;
    public bool lose;
    public bool win;
    private CameraFollow _cameraFollow;

    void Start()
    {
        perkChosen = false;
		lose = false;
        win = false;
		
        _cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        if (!perkChosen)
			_cameraFollow.lockRightEdge = true;
    }

    void Update()
    {
        CheckLost();
        CheckWin();
	}

	public void CheckWin()
	{
		//Checks if the boss health is 0 -- for alpha
		if (win)
		{
			UIManager.displayWin = true;
			PerkManager.UpdatePerkStatus(GlobalSettings.axe_dtVampirism_name, 1);
		}
	}

	public void CheckLost()
    {
        if (lose)
            UIManager.displayLost = true;
    }
}
