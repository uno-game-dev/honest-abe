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
    public class IntAbs : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The IntVar to change value")]
        public IntVar variable;

        public override void Reset () {
            variable = new ConcreteIntVar();
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