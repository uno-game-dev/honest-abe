//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a the variable value to the list.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Adds a the variable value to the list")]
    public class DynamicListAdd : ActionNode {

        /// <summary>
        /// The dynamic list to add the variable.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to add the variable")]
        public DynamicList list;

        /// <summary>
        /// The variable to be added to the list.
        /// </summary>
        [VariableInfo(tooltip = "The variable to be added to the list", fixedType = false)]
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
            list.Add(variable.genericValue);

            return Status.Success;
        }

    }
}