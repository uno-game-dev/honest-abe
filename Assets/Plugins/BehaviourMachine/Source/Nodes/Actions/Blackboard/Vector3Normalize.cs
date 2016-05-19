//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Normalize the "Variable".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Normalize the \"Variable\"")]
    public class Vector3Normalize : ActionNode {

        /// <summary>
        /// The variable to normalize.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to normalize")]
        public Vector3Var variable;

        public override void Reset () {
            variable = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            variable.Value = variable.Value.normalized;

            return Status.Success;
        }

    }
}