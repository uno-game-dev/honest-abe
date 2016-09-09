//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Removes a value from the list.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Removes a value from the list")]
    public class DynamicListRemove : ActionNode {

        /// <summary>
        /// The dynamic list to remove the value.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to remove the value")]
        public DynamicList list;

        /// <summary>
        /// The value to be removed from the list.
        /// </summary>
        [VariableInfo(tooltip = "The value to be removed from the list", fixedType = false)]
        public Variable variable;

        public override void Reset () {
            list = new ConcreteDynamicList();
            variable = new Variable();
        }

        public override Status Update () {
            // Validate members
            if (list.isNone || variable.isNone)
                return Status.Error;

            // Add to the list?
            list.Remove(variable.genericValue);

            return Status.Success;
        }

    }
}