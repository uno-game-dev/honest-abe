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
    public class IsStringNotEqual : ConditionNode {

        /// <summary>
        /// The first string.
        /// </summary>
    	[VariableInfo(tooltip = "The first string")]
        public StringVar a;

        /// <summary>
        /// The second string.
        /// </summary>
        [VariableInfo(tooltip = "The second string")]
        public StringVar b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteStringVar();
            b = new ConcreteStringVar();
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
