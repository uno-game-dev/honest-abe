using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class ShootWeapon : ConditionNode
{
    private Attack attack;

    public override void OnEnable()
    {
        attack = self.GetComponent<Attack>();
		attack.LightAttack();
    }

    public override Status Update()
    {
        if (attack.attackState == Attack.State.Null)
        {
            if (onSuccess.id != 0)
                owner.root.SendEvent(onSuccess.id);
            return Status.Success;
        }

        return Status.Running;
    }
}
