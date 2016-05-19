//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Divide "Variable" by "Divide By".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Divide \"Variable\" by \"Divide By\"")]
    public class FloatDivide : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The FloatVar to change value")]
        public FloatVar variable;

        /// <summary>
        /// The new variable value.
        /// </summary>
        [VariableInfo(tooltip = "Divide factor")]
        public FloatVar divideBy;

        public override void Reset () {
            variable = new ConcreteFloatVar();
            divideBy = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || divideBy.isNone)
                return Status.Error;

            variable.Value = variable.Value/divideBy.Value;

            return Status.Success;
        }
    }
}