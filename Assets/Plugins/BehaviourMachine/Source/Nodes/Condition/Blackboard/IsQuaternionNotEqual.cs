//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if "A" is not equal to "B"; returns Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if \"A\" is not equal to \"B\"; returns Failure otherwise")]
    public class IsQuaternionNotEqual : ConditionNode {

        /// <summary>
        /// The first Quaternion.
        /// </summary>
    	[VariableInfo(tooltip = "The first Quaternion")]
        public QuaternionVar a;

        /// <summary>
        /// The second Quaternion.
        /// </summary>
        [VariableInfo(tooltip = "The second Quaternion")]
        public QuaternionVar b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteQuaternionVar();
            b = new ConcreteQuaternionVar();
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone)
                return Status.Error;

            if (a.Value != b.Value) {
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
