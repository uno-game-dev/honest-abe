using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
