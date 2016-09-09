//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Tick its child for each value in the list (Does not guarantee order).
    /// </summary>
    [NodeInfo(  category = "Decorator/List/",
                icon = "PlayLoopOff",
                description = "Tick its child for each value in the list (Does not guarantee order)")]
    public class DynamicListForEach : DecoratorNode {

        /// <summary>
        /// The list to traverse.
        /// </summary>
        [VariableInfo(tooltip = "The list to traverse")]
        public DynamicList list;

        /// <summary>
        /// Stores the current value in the list.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the current value in the list", fixedType = false)]
        public Variable storeCurrentValue;

        public override void Reset () {
            list = new ConcreteDynamicList();
            storeCurrentValue = new Variable();
        }

        public override Status Update () {
            // Validate members
            if (child == null || list.isNone)
                return Status.Error;

            var childStatus = Status.Success;

            foreach (System.Object value in list.Value) {
                storeCurrentValue.genericValue = value;
                child.OnTick();
                childStatus = child.status;

                // Error?
                if (childStatus == Status.Error)
                    break;
            }

            return childStatus;
        }
    }
}