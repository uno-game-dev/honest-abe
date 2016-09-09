//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a float.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a float")]
    public class BoolToFloat : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The float value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The float value to be stored if the \"Bool Variable\" is True")]
        public FloatVar trueValue;

        /// <summary>
        /// The float value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The float value to be stored if the \"Bool Variable\" is False")]
        public FloatVar falseValue;

        /// <summary>
        /// Stores the float value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the float value")]
        public FloatVar storeFloat;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = 1f;
            falseValue = 0f;
            storeFloat = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeFloat.isNone)
                return Status.Error;

            storeFloat.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}