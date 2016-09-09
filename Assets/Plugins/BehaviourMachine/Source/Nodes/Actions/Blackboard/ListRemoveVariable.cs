//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Removes a variable from the list.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Removes a Variable from the list")]
    public class ListRemoveVariable : ActionNode {

        /// <summary>
        /// The dynamic list to remove the variable.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to remove the variable")]
        public DynamicList list;

        /// <summary>
        /// The variable to be removed.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be removed", fixedType = false)]
        public Variable variable;

        public override void Reset () {
            list = new ConcreteDynamicList();
            variable = new Variable();
        }

        public override Status Update () {
            // Validate members
            if (list.isNone)
                return Status.Error;

            // Remove from the list?
            list.Remove(variable);

            return Status.Success;
        }

    }
}