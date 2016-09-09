//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a Quaternion.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a Quaternion")]
    public class BoolToQuaternion : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The Quaternion value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The Quaternion value to be stored if the \"Bool Variable\" is True")]
        public QuaternionVar trueValue;

        /// <summary>
        /// The Quaternion value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The Quaternion value to be stored if the \"Bool Variable\" is False")]
        public QuaternionVar falseValue;

        /// <summary>
        /// Stores the Quaternion value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the Quaternion value")]
        public QuaternionVar storeQuaternion;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = Quaternion.Euler(0,0,180);
            falseValue = Quaternion.identity;
            storeQuaternion = new ConcreteQuaternionVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeQuaternion.isNone)
                return Status.Error;

            storeQuaternion.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}