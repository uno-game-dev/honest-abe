//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Stores "Variable.magnitude" in "Store Magnitude".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Stores \"Variable.magnitude\" in \"Store Magnitude\"")]
    public class Vector3GetMagnitude : ActionNode {

        /// <summary>
        /// The variable to get the magnitude.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to get the magnitude")]
        public Vector3Var variable;

        /// <summary>
        /// Stores the magnitude of \"Variable\".
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Stores the magnitude of \"Variable\"")]
        public FloatVar storeMagnitude;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            storeMagnitude = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            // Get value
            var value = variable.Value;
            storeMagnitude.Value = value.magnitude;

            return Status.Success;
        }

    }
}