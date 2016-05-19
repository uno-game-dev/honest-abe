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
    public class DynamicListGetIndexOf : ActionNode {

        /// <summary>
        /// The dynamic list to add the variable.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to add the variable")]
        public DynamicList list;

        /// <summary>
        /// The variable value to get the index.
        /// </summary>
        [VariableInfo(tooltip = "The variable value to get the index", fixedType = false)]
        public Variable variable;

        /// <summary>
        /// Stores the idex of the value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the idex of the value", fixedType = false)]
        public IntVar storeIndex;

        public override void Reset () {
            list = new ConcreteDynamicList();
            variable = new Variable();
            storeIndex = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (list.isNone || variable.isNone || storeIndex.isNone)
                return Status.Error;

            // Add to the list?
            storeIndex.Value = list.IndexOf(variable.genericValue);

            return Status.Success;
        }

    }
}