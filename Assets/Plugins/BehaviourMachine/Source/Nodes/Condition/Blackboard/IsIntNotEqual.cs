//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if "A" is not equal to "B" with tolerance; returns Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if \"A\" is not equal to \"B\" with tolerance; returns Failure otherwise")]
    public class IsIntNotEqual : ConditionNode {

        /// <summary>
        /// The first int.
        /// </summary>
    	[VariableInfo(tooltip = "The first int")]
        public IntVar a;

        /// <summary>
        /// The second int.
        /// </summary>
        [VariableInfo(tooltip = "The second int")]
        public IntVar b;

        /// <summary>
        /// The tolerance value.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The tolerance value")]
        public IntVar tolerance;

        public override void Reset () {
            base.Reset();

            a = new ConcreteIntVar();
            b = new ConcreteIntVar();
            tolerance = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone)
                return Status.Error;

            // Use tolerance?
            if (tolerance.isNone) {
                if (a.Value != b.Value) {
                    // Send event?
                    if (onSuccess.id != 0)
                        owner.root.SendEvent(onSuccess.id);

                    return Status.Success;
                }
                else
                    return Status.Failure;
            }
            else {
                if (Mathf.Abs(a.Value - b.Value) > tolerance.Value) {
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
}
