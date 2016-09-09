//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a string variable to an int.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a string variable to an int")]
    public class StringToInt : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(tooltip = "The variable to be converted")]
        public StringVar stringVariable;

        /// <summary>
        /// Stores the int value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the int value")]
        public IntVar storeInt;

        public override void Reset () {
            stringVariable = string.Empty;
            storeInt = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (stringVariable.isNone || storeInt.isNone)
                return Status.Error;

            storeInt.Value = int.Parse(stringVariable.Value);

            return Status.Success;
        }

    }
}