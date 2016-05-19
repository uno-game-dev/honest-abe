//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Call/Tick a FunctionNode in this tree. Returns the status of the ticked function or Error if the function could not be found.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "Function",
                description = "Call/Tick a FunctionNode in this tree. Returns the status of the ticked function or Error if the function could not be found")]
    public class TickFunctionInSelf : ActionNode {

        /// <summary>
        /// The name of the function to be ticked.
        /// </summary>
        [VariableInfo(tooltip = "The name of the function to be ticked.")]
        public StringVar functionName;

        public override void Reset () {
            functionName = "FunctionNode";
        }

        public override Status Update () {
            // Validate members
            if (functionName.isNone)
                return Status.Error;

            return tree.TickFunction(functionName.Value);
        }
    }
}
