using UnityEngine;
using System.Collections;

public class MainCameraKeyboardController : MonoBehaviour
{
    EnemyFollow _enemyFollow;

    void Start()
    {
        _enemyFollow = FindObjectOfType<EnemyFollow>();
    }

    // Temporary Debug Code below!
    void Update()
    {
        if (_enemyFollow && Input.GetKeyDown(KeyCode.F))
        {
            if (_enemyFollow.targetType == EnemyFollow.TargetType.Null)
                _enemyFollow.targetType = EnemyFollow.TargetType.Player;
            else
                _enemyFollow.targetType = EnemyFollow.TargetType.Null;
        }
    }
}
