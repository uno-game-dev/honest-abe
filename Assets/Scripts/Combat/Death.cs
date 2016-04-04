using UnityEngine;
using System.Collections;
using BehaviourMachine;
using System;

public class Death : MonoBehaviour
{
    public GameObject[] weaponDropPrefabs;
    public float chanceToDrop = 1;

    void OnEnable()
    {
        CharacterState characterState = GetComponent<CharacterState>();
        if (characterState) characterState.SetState(CharacterState.State.Dead);

        BaseCollision baseCollision = GetComponent<BaseCollision>();
        if (baseCollision) baseCollision.enabled = false;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider) collider.enabled = false;

        BehaviourTree[] behaviorTrees = GetComponents<BehaviourTree>();
        foreach (BehaviourTree behaviorTree in behaviorTrees)
            behaviorTree.enabled = false;

        EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
        if (enemyFollow) enemyFollow.enabled = false;

        WeaponDrop();
    }

    private void WeaponDrop()
    {
        if (weaponDropPrefabs.Length <= 0)
            return;

        if (UnityEngine.Random.value > chanceToDrop)
            return;

        int i = UnityEngine.Random.Range(0, weaponDropPrefabs.Length - 1);
        GameObject dismemberWeapon = Instantiate(weaponDropPrefabs[i]);
        dismemberWeapon.transform.SetParent(transform.parent);
        dismemberWeapon.transform.position = transform.position;
        dismemberWeapon.transform.Translate(0, -1, 0);
        dismemberWeapon.transform.localScale = Vector3.one;

    }
}
