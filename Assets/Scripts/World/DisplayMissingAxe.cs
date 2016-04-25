using UnityEngine;

public class DisplayMissingAxe : MonoBehaviour
{
    Weapon.AttackType _attackType;

    // Use this for initialization
    void Start()
    {
        _attackType = GameObject.Find("Player").GetComponent<Attack>().weapon.attackType;

        foreach (Transform child in transform)
        {
            if (_attackType == Weapon.AttackType.Melee && child.GetComponent<Perk>().type == PerkManager.activeAxePerk.type)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
