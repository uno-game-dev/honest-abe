using UnityEngine;
using System.Collections;
using System;

public class MainCameraKeyboardController : MonoBehaviour
{
    public static event Action LightAttack = delegate { };
    public static event Action HeavyAttack = delegate { };

    EnemyFollow _enemyFollow;

    void Start()
    {
        _enemyFollow = FindObjectOfType<EnemyFollow>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            LightAttack();

        if (Input.GetButtonDown("Fire2"))
            HeavyAttack();

        if (_enemyFollow && Input.GetKeyDown(KeyCode.F))
        {
            if (_enemyFollow.targetType == EnemyFollow.TargetType.Null)
                _enemyFollow.targetType = EnemyFollow.TargetType.Player;
            else
                _enemyFollow.targetType = EnemyFollow.TargetType.Null;
        }
    }
}
