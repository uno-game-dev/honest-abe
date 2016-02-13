using UnityEngine;

public interface IAttackType
{
    Vector2 Position { get; }
    Vector2 Size { get; }
    float Damage { get; }

    void LightAttack();
    void HeavyAttack();
}