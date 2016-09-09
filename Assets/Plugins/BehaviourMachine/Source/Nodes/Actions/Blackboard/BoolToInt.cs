//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to an int.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to an int")]
    public class BoolToInt : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The int value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The int value to be stored if the \"Bool Variable\" is True")]
        public IntVar trueValue;

        /// <summary>
        /// The int value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The int value to be stored if the \"Bool Variable\" is False")]
        public IntVar falseValue;

        /// <summary>
        /// Stores the int value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the int value")]
        public IntVar storeInt;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = 1;
            falseValue = 0;
            storeInt = new ConcreteIntVar();
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