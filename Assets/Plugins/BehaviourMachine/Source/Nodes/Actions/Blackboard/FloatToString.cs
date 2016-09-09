//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a float variable to a string.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a float variable to a string")]
    public class FloatToString : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(tooltip = "The variable to be converted")]
        public FloatVar floatVariable;

        /// <summary>
        /// An optionally format to convert the float value. E.g. 000, 00.00.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "An optionally format to convert the float value. E.g. 000, 00.00")]
        public StringVar format;

        /// <summary>
        /// Stores the string value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the string value")]
        public StringVar storeString;

        public override void Reset () {
            floatVariable = 0f;
            storeString = new ConcreteStringVar();
            format = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (floatVariable.isNone || storeString.isNone)
                return Status.Error;

            // The format is valid?
            if (!format.isNone && !string.IsNullOrEmpty(format.Value)) 
                storeString.Value = floatVariable.Value.ToString(format.Value);
            else
                storeString.Value = floatVariable.Value.ToString();

            return Status.Success;
        }

    }
}