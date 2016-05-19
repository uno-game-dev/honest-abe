//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Executes its child while the "Condition" value is True.
    /// Be aware that you need to update the value of the condition in the child; or you will end up with an infinite loop.
    /// </summary>
    [NodeInfo ( category = "Decorator/",
                icon = "PlayLoopOff",
                description = "Executes its child while the \"Condition\" value is True. Be aware that you need to update the value of the condition in the child; or you will end up with an infinite loop")]
    public class While : DecoratorNode {

        /// <summary>
        /// The condition to evaluate.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The condition to evaluate")]
        public BoolVar condition;

        public override void Reset () {
            condition = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate members
           if (child == null || condition.isNone)
                return Status.Error;

            var childStatus = Status.Success;

            while (condition.Value) {
                child.OnTick();
                childStatus = child.status;
                if (childStatus == Status.Error || childStatus == Status.Running)
                    break;
            }

            return childStatus;
        }
    }
}