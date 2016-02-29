using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    private Vector3 abeLocation;
    private GameObject cam;

    // Use this for initialization
    void Start()
    {
        AudioManager.instance.PlayBossSound(1);
    }

    // Update is called once per frame
    void Update()
    {
        abeLocation = GameObject.Find("Player").transform.position;
        if ((gameObject.transform.position.x - abeLocation.x) < 10)
        {
            //The boss is in the scene with Abe so lock the camera
            GlobalSettings.bossFight = true;
        }
    }
}
