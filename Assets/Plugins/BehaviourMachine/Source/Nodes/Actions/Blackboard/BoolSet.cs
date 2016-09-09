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
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Changes the \"Variable\" value")]
    public class BoolSet : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public BoolVar variable;

        /// <summary>
        /// The new variable value. If Toggle is selected the value of "variable" is flipped.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The new variable value. If Toggle is selected the value of \"variable\" is flipped")]
        public BoolVar newValue;

        public override void Reset () {
            variable = new ConcreteBoolVar();
            newValue = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            // Flip value?
            variable.Value = newValue.isNone ? !variable.Value : newValue.Value;

            return Status.Success;
        }

    }
}