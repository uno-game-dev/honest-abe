using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum AttackType { Melee, Swing, Jab, Shoot, Slash, Projectile, Knife, Pistol }

    public AttackType attackType = AttackType.Melee;
    public Vector2 attackSize = Vector2.one;
    public Vector2 attackOffset = new Vector2(1, 0);
    public Vector3 heldOffset = Vector3.zero;
    public Vector3 heldOrientation = Vector3.zero;

    public float lightDamage = 10;
	public float lightStun = 0.2f;
	public float lightKnockback = 4f;

    public float heavyDamage = 15;
	public float heavyStun = 0.5f;
	public float heavyKnockback = 7f;
	public float throwDamage = 0;

    public bool isEnemyWeapon = false;
}
