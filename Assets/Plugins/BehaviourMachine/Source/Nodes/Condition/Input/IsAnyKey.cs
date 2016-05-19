//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Is any key or mouse button currently held down?
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Keyboard",
                description = "Is any key or mouse button currently held down?")]
    public class IsAnyKey : ConditionNode {

        public override Status Update () {
            if (Input.anyKey) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}