//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the "Variable" value.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Changes the \"Variable\" value")]
    public class Vector3SetXYZ : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public Vector3Var variable;

        /// <summary>
        /// The new variable.x.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new variable.x")]
        public FloatVar newX;

        /// <summary>
        /// The new variable.y.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new variable.y")]
        public FloatVar newY;

        /// <summary>
        /// The new variable.z.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new variable.z")]
        public FloatVar newZ;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            newX = new ConcreteFloatVar();
            newY = new ConcreteFloatVar();
            newZ = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            // Set values
            var newValue = variable.Value;

            if (!newX.isNone) newValue.x = newX.Value;
            if (!newY.isNone) newValue.y = newY.Value;
            if (!newZ.isNone) newValue.z = newZ.Value;

            variable.Value = newValue;

            return Status.Success;
        }

    }
}