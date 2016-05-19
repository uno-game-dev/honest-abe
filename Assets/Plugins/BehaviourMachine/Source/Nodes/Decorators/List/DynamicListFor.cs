//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Tick its children for each value in the DynamicList; starts from the first (or the last) value and guarantee order.
    /// </summary>
    [NodeInfo(  category = "Decorator/List/",
                icon = "PlayLoopOff",
                description = "Tick its children for each value in the DynamicList; starts from the first (or the last) value and guarantee order")]
    public class DynamicListFor : DecoratorNode {

        /// <summary>
        /// The list to traverse.
        /// </summary>
        [VariableInfo(tooltip = "The list to traverse")]
        public DynamicList list;

        /// <summary>
        /// Stores the current value index.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the current value index")]
        public IntVar storeIndex;

        /// <summary>
        /// Stores the current value in the list.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the current value in the list", fixedType = false)]
        public Variable storeCurrentValue;


        /// <summary>
        /// If True then the list will be traversed backwards starting from the last value; very useful when you need to delete values from the list.
        /// </summary>
        [Tooltip("If True then the list will be traversed backwards starting from the last value; very useful when you need to delete values from the list")]
        public bool reverse = false;

        public override void Reset () {
            list = new ConcreteDynamicList();
            storeIndex = new ConcreteIntVar();
            storeCurrentValue = new Variable();
            reverse = false;
        }

        public override Status Update () {
            // Validate members
            if (child == null || list.isNone)
                return Status.Error;

            var childStatus = Status.Success;

            if (!reverse) {
                for (int i = 0; i < list.Count; i++) {
                    // Tick child
                    storeCurrentValue.genericValue = list[i];
                    storeIndex.genericValue = i;
                    child.OnTick();

                    // Error?
                    childStatus = child.status;
                    if (childStatus == Status.Error)
                        break;
                }
            }
            else {
                for (int i = list.Count - 1; i >= 0; i--) {
                    // Tick child
                    storeCurrentValue.genericValue = list[i];
                    storeIndex.genericValue = i;
                    child.OnTick();

                    // Error?
                    childStatus = child.status;
                    if (childStatus == Status.Error)
                        break;
                }
            }

            return childStatus;
        }
    }
}