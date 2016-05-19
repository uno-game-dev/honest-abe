//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if at least one BoolVar in "Values" is true; otherwise returns Failure.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if at least one BoolVar in \"Values\" is true; otherwise returns Failure")]
    public class IsAnyBoolTrue : ConditionNode {

        /// <summary>
        /// The booleans to check.
        /// </summary>
        [VariableInfo(canBeConstant = false)]
        public BoolVar[] values;

        public override void Reset () {
            base.Reset();
            values = new BoolVar[] {new ConcreteBoolVar()};
        }

        public override Status Update () {
            // Validate parameters
            if (values.Length == 0)
                return Status.Error;

            // Validate values
            for (int i = 0; i < values.Length; i++) {
                if (values[i].isNone)
                    return Status.Error;
                else if (values[i].Value) {
                    // Send event?
                    if (onSuccess.id != 0)
                        owner.root.SendEvent(onSuccess.id);

                    return Status.Success;
                }
            }

            return Status.Failure;
        }
    }
}