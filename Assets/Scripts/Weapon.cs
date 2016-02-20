using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public enum AttackType { Melee, Swing, Jab, Gun, Projectile }

    public AttackType attackType = AttackType.Melee;
    public Vector2 attackSize = Vector2.one;
    public Vector2 attackOffset = new Vector2(1, 0.5f);
    public Vector3 heldOrientation = Vector3.zero;
    public float lightDamage = 10;
    public float heavyDamage = 15;

	public void OnCollision(GameObject other)
	{
        if (other.tag == "Player") {
            other.GetComponent<Attack>().SetWeapon(this);
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}
}
