using UnityEngine;
using BehaviourMachine;

public class Death : MonoBehaviour
{
    public GameObject[] weaponDropPrefabs;
    public GameObject[] itemDropPrefabs;
    public float chanceToDrop = 1;

    private Renderer _renderer;
    private System.Random _rnd;
    private bool _seen = false;

    void OnEnable()
    {
        SoundPlayer sound = GetComponent<SoundPlayer>();
        SoundPlayer.Play("Death");

        CharacterState characterState = GetComponent<CharacterState>();
        if (characterState) characterState.SetState(CharacterState.State.Dead);

        BaseCollision baseCollision = GetComponent<BaseCollision>();
        if (baseCollision) baseCollision.enabled = false;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider) collider.enabled = false;

        Movement movement = GetComponent<Movement>();
        movement.enabled = false;

        BehaviourTree[] behaviorTrees = GetComponents<BehaviourTree>();
        foreach (BehaviourTree behaviorTree in behaviorTrees)
            behaviorTree.enabled = false;

        EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
        if (enemyFollow) enemyFollow.enabled = false;

        WeaponDrop();
        ItemDrop();
        
        gameObject.AddComponent<ObjectDestroyer>();

        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_renderer)
        {
            if (_renderer.isVisible)
                _seen = true;
            if (_seen && !_renderer.isVisible)
                Destroy(gameObject);
        }
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
        _rnd = new System.Random();
        int r = _rnd.Next(101);
        if (r >= 75)
        {
            r = _rnd.Next(itemDropPrefabs.Length);
            Instantiate(itemDropPrefabs[r], gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            Debug.Log("Item " + r + " dropped");
        }
    }
}
