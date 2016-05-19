//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success the first frame the user hits any key or mouse button.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Keyboard",
                description = "Returns Success the first frame the user hits any key or mouse button")]
    public class IsAnyKeyDown : ConditionNode {

        public override Status Update () {
            if (Input.anyKeyDown) {
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