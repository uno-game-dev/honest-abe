//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Removes all elements from the list.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Removes all elements from the list")]
    public class DynamicListClear : ActionNode {

        /// <summary>
        /// The dynamic list to clear.
        /// </summary>
        [VariableInfo(tooltip = "The target dynamic list to clear")]
        public DynamicList list;


        public override void Reset () {
            list = new ConcreteDynamicList();
        }

        public override Status Update () {
            // Validate members
            if (list.isNone)
                return Status.Error;

            // Add to the list?
            list.Clear();

            return Status.Success;
        }

    }
}