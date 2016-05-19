//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if "A" is greater than or equals to "B"; returns Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if \"A\" is greater than or equals to \"B\"; returns Failure otherwise")]
    public class IsFloatGreaterOrEqual : ConditionNode {

        /// <summary>
        /// The first float.
        /// </summary>
    	[VariableInfo(tooltip = "The first float")]
        public FloatVar a;

        /// <summary>
        /// The second float.
        /// </summary>
        [VariableInfo(tooltip = "The second float")]
        public FloatVar b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteFloatVar();
            b = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone)
                return Status.Error;

            if (a.Value >= b.Value) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}
