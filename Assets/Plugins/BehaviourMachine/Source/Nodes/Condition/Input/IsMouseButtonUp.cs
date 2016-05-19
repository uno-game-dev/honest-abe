//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success during the frame the user releases the given mouse button.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Mouse",
                description = "Returns Success during the frame the user releases the given mouse button")]
    public class IsMouseButtonUp : ConditionNode {

        /// <summary>
        /// The mouse button to test.
        /// <summary>
        [Tooltip("The mouse button to test")]
        public MouseButton mouseButton;

        public override void Reset () {
            base.Reset();

            mouseButton = MouseButton.Left;
        }

        public override Status Update () {
            if (Input.GetMouseButtonUp((int)mouseButton)) {
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