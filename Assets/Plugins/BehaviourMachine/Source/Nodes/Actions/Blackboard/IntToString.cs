//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts an int variable to a string.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts an int variable to a string")]
    public class IntToString : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(tooltip = "The variable to be converted")]
        public IntVar intVariable;

        /// <summary>
        /// An optionally format to convert the int value. E.g. 000.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "An optionally format to convert the int value. E.g. 000")]
        public StringVar format;

        /// <summary>
        /// Stores the string value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the string value")]
        public StringVar storeString;

        public override void Reset () {
            intVariable = 0;
            storeString = new ConcreteStringVar();
            format = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (intVariable.isNone || storeString.isNone)
                return Status.Error;

            // The format is valid?
            if (!format.isNone && !string.IsNullOrEmpty(format.Value)) 
                storeString.Value = intVariable.Value.ToString(format.Value);
            else
                storeString.Value = intVariable.Value.ToString();

            return Status.Success;
        }

    }
}