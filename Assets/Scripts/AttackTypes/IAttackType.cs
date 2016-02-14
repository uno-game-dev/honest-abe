using UnityEngine;

public interface IAttackType
{
    void LightAttack();
    void HeavyAttack();

    Weapon Weapon { get; set; }
}