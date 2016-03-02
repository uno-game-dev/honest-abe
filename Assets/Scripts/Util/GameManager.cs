using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool perkChosen;
    public static bool lose;
    public static bool win;
    private GameObject _camera;

    private CameraFollow cameraFollow;

    void Start()
    {
        perkChosen = false;
		lose = false;
        win = false;

        _camera = GameObject.Find("Main Camera");
        cameraFollow = _camera.GetComponent<CameraFollow>();
        if (!perkChosen) cameraFollow.lockRightEdge = true;
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
