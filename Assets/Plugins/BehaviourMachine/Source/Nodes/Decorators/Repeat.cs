//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Tick its child a number of "Times".
    /// </summary>
    [NodeInfo(  category = "Decorator/",
                icon = "PlayLoopOff",
                description = "Tick its child a number of \"Times\"")]
    public class Repeat : DecoratorNode {

        /// <summary>
        /// Number of times to execute the child.
        /// </summary>
        [VariableInfo(tooltip = "Number of times to tick the child")]
        public IntVar times;

        /// <summary>
        /// Optional variable to store the current index of the execution.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Optional variable to store the current index of the execution")]
        public IntVar index;

        public override void Reset () {
            times = new ConcreteIntVar();
            index = new ConcreteIntVar();
        }


        public override Status Update () {
            // Validate members
            if (child == null || times.isNone)
                return Status.Error;

            var childStatus = Status.Success;

            for (index.Value = 0; index.Value < times.Value && childStatus != Status.Error; index.Value++) {
                child.OnTick();
                childStatus = child.status;
            }

            return childStatus;
        }
    }
}