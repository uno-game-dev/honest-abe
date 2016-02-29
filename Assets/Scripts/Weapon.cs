using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum AttackType { Melee, Swing, Jab, Gun, Projectile }

    public AttackType attackType = AttackType.Melee;
    public Vector2 attackSize = Vector2.one;
    public Vector2 attackOffset = new Vector2(1, 0);
    public Vector3 heldOffset = Vector3.zero;
    public Vector3 heldOrientation = Vector3.zero;
    public float lightDamage = 10;
    public float heavyDamage = 15;
}
