//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts an int variable to a float.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts an int variable to a float")]
    public class IntToFloat : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(tooltip = "The variable to be converted")]
        public IntVar intVariable;

        /// <summary>
        /// Stores the float value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the float value")]
        public FloatVar storeFloat;


        public override void Reset () {
            intVariable = 0;
            storeFloat = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (intVariable.isNone || storeFloat.isNone)
                return Status.Error;

            storeFloat.Value = (float)intVariable.Value;

            return Status.Success;
        }

    }
}