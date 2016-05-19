//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets "Variable" to its absolute value.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Sets \"Variable\" to its absolute value")]
    public class FloatAbs : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The FloatVar to change value")]
        public FloatVar variable;

        public override void Reset () {
            variable = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            variable.Value = Mathf.Abs(variable.Value);

            return Status.Success;
        }
    }
}