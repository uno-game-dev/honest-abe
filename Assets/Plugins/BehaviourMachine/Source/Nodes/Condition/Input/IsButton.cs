//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success while the virtual button identified by Button Name is held down.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Button",
                description = "Returns Success while the virtual button identified by Button Name is held down")]
    public class IsButton : ConditionNode {

        /// <summary>
        /// The virtual button to test.
        /// <summary>
        [VariableInfo(tooltip = "The virtual button to test")]
        public StringVar buttonName;

        public override void Reset () {
            base.Reset();

            buttonName = "Fire1";
        }

        public override Status Update () {
            // Validate members
            if (buttonName.isNone)
                return Status.Error;

            if (Input.GetButton(buttonName.Value)) {
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