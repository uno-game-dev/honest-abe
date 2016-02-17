using UnityEngine;

public interface IAttackType
{
    void LightAttack();
    void HeavyAttack();

    Weapon Weapon { get; set; }
    Animator Animator { get; set; }
    Attack Attack { get; set; }
    GameObject AttackArea { get; set; }
}