using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public enum AttackStrength { Light, Heavy }

    public Queue<AttackStrength> input = new Queue<AttackStrength>();
    public int maxQueueSize = 4;

    public virtual void AddAttack(AttackStrength strength) { }

    public Weapon Weapon { get; set; }
    public Animator Animator { get; set; }
    public Attack Attack { get; set; }
    public GameObject AttackArea { get; set; }
}