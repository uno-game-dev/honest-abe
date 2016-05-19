//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if "A" is not equal to "B"; returns Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if \"A\" is not equal to \"B\"; returns Failure otherwise")]
    public class IsObjectNotEqual : ConditionNode {

        /// <summary>
        /// The first Object.
        /// </summary>
    	[VariableInfo(tooltip = "The first Object")]
        public ObjectVar a;

        /// <summary>
        /// The second Object.
        /// </summary>
        [VariableInfo(tooltip = "The second Object")]
        public ObjectVar b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteObjectVar();
            b = new ConcreteObjectVar();
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone)
                return Status.Error;

            if (a.Value != b.Value) {
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
