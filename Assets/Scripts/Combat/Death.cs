using UnityEngine;
using BehaviourMachine;

public class Death : MonoBehaviour
{
    public GameObject[] weaponDropPrefabs;
    public GameObject[] itemDropPrefabs;
    public float chanceToDrop = 1;

    private System.Random _rnd;

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
        ItemDrop();
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

    private void ItemDrop()
    {
        int r = _rnd.Next(101);
        if (r >= 75)
        {
            r = _rnd.Next(itemDropPrefabs.Length);
            Instantiate(itemDropPrefabs[r], gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
}
