//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if the DynamicList has the supplied value.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if the DynamicList has the supplied value")]
    public class DynamicListContains : ConditionNode {

        /// <summary>
        /// The dynamic list to search for the value.
        /// </summary>
        [VariableInfo(tooltip = "The dynamic list to search for the value")]
        public DynamicList list;

        /// <summary>
        /// The variable to search for.
        /// </summary>
        [VariableInfo(tooltip = "The variable to search for", fixedType = false)]
        public Variable variable;

        public override void Reset () {
            list = new ConcreteDynamicList();
            variable = new Variable();
        }

        public override Status Update () {
            // Validate members
            if (list.isNone || variable.isNone)
                return Status.Error;

            // Add to the list?
            if (list.Contains(variable.genericValue)) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else {
                return Status.Failure;
            }
        }

    }
}