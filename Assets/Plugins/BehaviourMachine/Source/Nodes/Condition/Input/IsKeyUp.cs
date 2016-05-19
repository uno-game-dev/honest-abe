//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success during the frame the user releases the "Key Code"; otherwise returns Failure.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Keyboard",
                description = "Returns Success during the frame the user releases the \"Key Code\"; otherwise returns Failure")]
    public class IsKeyUp : ConditionNode {

        /// <summary>
        /// The key to test.
        /// <summary>
        [Tooltip("The key to test")]
        public KeyCode keyCode;

        public override void Reset () {
            base.Reset();

            keyCode = KeyCode.None;
        }

        public override Status Update () {
            if (Input.GetKeyUp(keyCode)) {
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