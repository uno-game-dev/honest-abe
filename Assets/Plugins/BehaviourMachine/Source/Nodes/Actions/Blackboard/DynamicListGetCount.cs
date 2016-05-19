//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the number of values stored in the list.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Gets the number of values stored in the list")]
    public class DynamicListGetCount : ActionNode {

        /// <summary>
        /// The dynamic list to add the variable.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to add the variable")]
        public DynamicList list;

        /// <summary>
        /// Stores the number of values in the list.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the number of values in the list", fixedType = false)]
        public IntVar storeCount;

        public override void Reset () {
            list = new ConcreteDynamicList();
            storeCount = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (list.isNone || storeCount.isNone)
                return Status.Error;

            // Add to the list?
            storeCount.Value = list.Count;

            return Status.Success;
        }

    }
}