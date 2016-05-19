//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a Object.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a Object")]
    public class BoolToObject : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The Object value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The Object value to be stored if the \"Bool Variable\" is True")]
        public ObjectVar trueValue;

        /// <summary>
        /// The Object value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The Object value to be stored if the \"Bool Variable\" is False")]
        public ObjectVar falseValue;

        /// <summary>
        /// Stores the Object value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the Object value")]
        public ObjectVar storeObject;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = (UnityEngine.Object)null;
            falseValue = (UnityEngine.Object)null;
            storeObject = new ConcreteObjectVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeObject.isNone)
                return Status.Error;

            storeObject.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}