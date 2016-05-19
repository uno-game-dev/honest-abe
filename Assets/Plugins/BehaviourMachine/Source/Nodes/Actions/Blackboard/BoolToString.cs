//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a string.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a string")]
    public class BoolToString : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The string value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The string value to be stored if the \"Bool Variable\" is True")]
        public StringVar trueValue;

        /// <summary>
        /// The string value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The string value to be stored if the \"Bool Variable\" is False")]
        public StringVar falseValue;

        /// <summary>
        /// Stores the string value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the string value")]
        public StringVar storeInt;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = "True";
            falseValue = "False";
            storeInt = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeInt.isNone)
                return Status.Error;

            storeInt.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}