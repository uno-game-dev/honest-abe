using UnityEngine;
using System.Collections;

public class MainCameraKeyboardController : MonoBehaviour
{
    EnemyFollow _enemyFollow;
    EnemyFollowWithBaseCollision _enemyFollow2;

    void Start()
    {
        _enemyFollow = FindObjectOfType<EnemyFollow>();
        _enemyFollow2 = FindObjectOfType<EnemyFollowWithBaseCollision>();
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
        else if (_enemyFollow2 && Input.GetKeyDown(KeyCode.F))
        {
            if (_enemyFollow2.targetType == EnemyFollowWithBaseCollision.TargetType.Null)
                _enemyFollow2.targetType = EnemyFollowWithBaseCollision.TargetType.Player;
            else
                _enemyFollow2.targetType = EnemyFollowWithBaseCollision.TargetType.Null;
        }
    }
}
