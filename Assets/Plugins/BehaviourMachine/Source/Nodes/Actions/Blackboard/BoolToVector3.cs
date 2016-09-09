//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a Vector3.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a Vector3")]
    public class BoolToVector3 : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The Vector3 value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The Vector3 value to be stored if the \"Bool Variable\" is True")]
        public Vector3Var trueValue;

        /// <summary>
        /// The Vector3 value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The Vector3 value to be stored if the \"Bool Variable\" is False")]
        public Vector3Var falseValue;

        /// <summary>
        /// Stores the Vector3 value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the Vector3 value")]
        public Vector3Var storeVector3;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = new Vector3(1,1,1);
            falseValue = Vector3.zero;
            storeVector3 = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeVector3.isNone)
                return Status.Error;

            storeVector3.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}