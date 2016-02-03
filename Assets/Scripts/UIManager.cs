using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static bool updateActive = false;

    private GameObject startGameText;

    void Start() {
        startGameText = GameObject.Find("StartText");
    }

	void Update () {
        if (!updateActive && Input.GetKeyDown(KeyCode.Return)) {
            updateActive = true;
            startGameText.SetActive(false);
        }
	}
}
