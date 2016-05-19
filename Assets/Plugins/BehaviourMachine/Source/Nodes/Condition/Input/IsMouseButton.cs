//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    public enum MouseButton {Left = 0, Right = 1, Middle = 2}

    /// <summary>
    /// Returns Success whether the given mouse button is held down.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Mouse",
                description = "Returns Success whether the given mouse button is held down")]
    public class IsMouseButton : ConditionNode {

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
            if (Input.GetMouseButton((int)mouseButton)) {
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