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
    private bool seenPlayer = false;
    private CharacterState _characterState;

    private void Start()
    {
        _enemyFollow = GetComponent<EnemyFollow>();
    }

    void Awake()
    {
        _enemyFollow = GetComponent<EnemyFollow>();
        _attack = GetComponent<Attack>();
        _player = GameObject.Find("Player");
        _characterState = GetComponent<CharacterState>();
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
            if (seenPlayer)
            {
                if (_characterState.state == CharacterState.State.Attack)
                {
                    _enemyFollow.targetType = EnemyFollow.TargetType.Null;
                }
                else {
                    if (!AttackProximityCheck())
                    {
                        _enemyFollow.targetType = EnemyFollow.TargetType.Player;
                    }
                    else {
                        if (Random.value > 0.35)
                        {
                            _attack.LightAttack();
                            yield return new WaitForSeconds(0.5f);
                        }
                        else {
                            _attack.HeavyAttack();
                            yield return new WaitForSeconds(2f);
                        }
                    }
                }
            }
            else {
                if (MovementProximityCheck())
                    seenPlayer = true;
            }


            //yield return new WaitForSeconds(Random.value * 3f);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
