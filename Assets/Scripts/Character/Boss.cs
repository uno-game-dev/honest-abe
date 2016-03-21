using UnityEngine;

public class Boss : MonoBehaviour
{
    public string bossName;

    private CameraFollow _cameraFollow;
    private Vector3 _playerPosition;

    // Use this for initialization
    void Start()
    {
        _cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerPosition = GameObject.Find("Player").transform.position;
        if ((gameObject.transform.position.x - _playerPosition.x) < 10)
        {
            //The boss is in the scene with Abe so lock the camera
            _cameraFollow.lockRightEdge = true;
            GameObject.Find("UI").GetComponent<UIManager>().bossHealthUI.enabled = true;
        }
    }
}