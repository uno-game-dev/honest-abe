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
    public class IntDivide : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The IntVar to change value")]
        public IntVar variable;

        /// <summary>
        /// The new variable value.
        /// </summary>
        [VariableInfo(tooltip = "Divide factor")]
        public IntVar divideBy;

        public override void Reset () {
            variable = new ConcreteIntVar();
            divideBy = new ConcreteIntVar();
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