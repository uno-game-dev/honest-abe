//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Removes the value in the supplied index from the list.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Removes the value in the supplied index from the list")]
    public class DynamicListRemoveAt : ActionNode {

        /// <summary>
        /// The dynamic list to remove the value.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to remove the value")]
        public DynamicList list;

        /// <summary>
        /// The index of the value to be removed.
        /// </summary>
        [VariableInfo(tooltip = "The index of the value to be removed", fixedType = false)]
        public IntVar index;

        public override void Reset () {
            list = new ConcreteDynamicList();
            index = 0;
        }

        public override Status Update () {
            // Validate members
            if (list.isNone || index.isNone)
                return Status.Error;

            // Add to the list?
            list.RemoveAt(index.Value);

            return Status.Success;
        }

    }
}