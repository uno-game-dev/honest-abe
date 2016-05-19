//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Inserts a value to the list in the supplied index.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Inserts a value to the list in the supplied index")]
    public class DynamicListInsert : ActionNode {

        /// <summary>
        /// The dynamic list to add the value.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to add the value")]
        public DynamicList list;

        /// <summary>
        /// The value to be inserted in the list.
        /// </summary>
        [VariableInfo(tooltip = "The value to be inserted in the list", fixedType = false)]
        public Variable variable;

        /// <summary>
        /// The index to insert the value, should be less than the list count.
        /// </summary>
        [VariableInfo(tooltip = "The index to insert the value, should be less than the list count")]
        public IntVar index;

        public override void Reset () {
            list = new ConcreteDynamicList();
            variable = new Variable();
            index = 0;
        }

        public override Status Update () {
            // Validate members
            if (list.isNone || variable.isNone || index.isNone)
                return Status.Error;

            // Insert into the list?
            list.Insert(index.Value, variable.genericValue);

            return Status.Success;
        }

    }
}