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
    public class IsVector3NotEqual : ConditionNode {

        /// <summary>
        /// The first Vector3.
        /// </summary>
    	[VariableInfo(tooltip = "The first Vector3")]
        public Vector3Var a;

        /// <summary>
        /// The second Vector3.
        /// </summary>
        [VariableInfo(tooltip = "The second Vector3")]
        public Vector3Var b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteVector3Var();
            b = new ConcreteVector3Var();
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
