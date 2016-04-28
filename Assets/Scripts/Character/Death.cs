using UnityEngine;
using BehaviourMachine;

public class Death : MonoBehaviour
{
    public GameObject[] weaponDropPrefabs;
    public GameObject[] itemDropPrefabs;
    private float chanceToDrop = .20f;
	private float randomNum = 0;

    private Renderer _renderer;
    private System.Random _rnd;
    private bool _seen = false;
	private int weaponOfRileman;

    void OnEnable()
    {
        CharacterState characterState = GetComponent<CharacterState>();
        if (characterState) characterState.SetState(CharacterState.State.Dead);

        SoundPlayer sound = GetComponent<SoundPlayer>();
        SoundPlayer.Play("Death");

        Animator animator = GetComponent<Animator>();
        animator.TransitionPlay("Dead");

        foreach (MonoBehaviour monoBehaviour in GetComponents<MonoBehaviour>())
            if (monoBehaviour != this)
                monoBehaviour.enabled = false;
        
        Collider2D collider = GetComponent<Collider2D>();
        if (collider) collider.enabled = false;

		//Disable the original gun that the rifleman carries 
		if (gameObject.name.Contains("Rifleman") && (gameObject.transform.FindContainsInChildren("Musket") != null)) {
			gameObject.transform.FindContainsInChildren("Musket").gameObject.SetActive(false);
		}

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
		randomNum = UnityEngine.Random.value;
		if (weaponDropPrefabs.Length <= 0)
            return;
		if(randomNum > chanceToDrop)
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
