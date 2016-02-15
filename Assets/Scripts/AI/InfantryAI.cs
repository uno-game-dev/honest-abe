using UnityEngine;
using System.Collections;

public class InfantryAI : MonoBehaviour
{
    public float movementProximityDistance = 12;
    public float attackProximityDistanceX = 3;
    public float attackProximityDistanceY = 1;

    private EnemyFollow _enemyFollow;
    private GameObject _player;
    private Attack _attack;

    void Awake()
    {
        _enemyFollow = GetComponent<EnemyFollow>();
        _attack = GetComponent<Attack>();
        _player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        StartCoroutine("DoCheck");
    }

    void OnDisable()
    {
        StopCoroutine("DoCheck");
    }

    private bool AttackProximityCheck()
    {
        if (Mathf.Abs(_player.transform.position.x - transform.position.x) < attackProximityDistanceX)
            if (Mathf.Abs(_player.transform.position.y - transform.position.y) < attackProximityDistanceY)
                return true;

        return false;
    }

    private bool MovementProximityCheck()
    {
        if (Mathf.Abs(_player.transform.position.x - transform.position.x) < movementProximityDistance)
            return true;
        else
            return false;
    }

    IEnumerator DoCheck()
    {
        while (true)
        {
            if (MovementProximityCheck())
                _enemyFollow.targetType = EnemyFollow.TargetType.Player;
            else
                _enemyFollow.targetType = EnemyFollow.TargetType.Null;

            if (AttackProximityCheck())
            {
                yield return new WaitForSeconds(Random.value * 3f);
                _attack.LightAttack();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
