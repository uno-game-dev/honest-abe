//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Inverts a Vector3 direction.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Inverts a Vector3 direction")]
    public class Vector3Invert : ActionNode {

        /// <summary>
        /// The Vector3 to change the direction.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The Vector3 to change the direction")]
        public Vector3Var variable;

        public override void Reset () {
            variable = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            variable.Value = -1 * variable.Value;

            return Status.Success;
        }

    }
}