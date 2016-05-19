//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if "A" is not equals to "B" with tolerance; returns Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if \"A\" is not equals to \"B\" with tolerance; returns Failure otherwise")]
    public class IsFloatNotEqual : ConditionNode {

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

        /// <summary>
        /// The tolerance value.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The tolerance value")]
        public FloatVar tolerance;

        public override void Reset () {
            base.Reset();

            a = new ConcreteFloatVar();
            b = new ConcreteFloatVar();
            tolerance = new ConcreteFloatVar();
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
