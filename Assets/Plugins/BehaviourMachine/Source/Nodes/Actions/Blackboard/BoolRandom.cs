//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a boolean value.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a boolean value")]
    public class BoolRandom : ActionNode {

        /// <summary>
        /// Store the random selected booelan.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected booelan")]
        public BoolVar storeBool;

        public override void Reset () {
            storeBool = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate members
            if (storeBool.isNone)
                return Status.Error;

            // Randomly selects a boolean
            storeBool.Value = Random.value > .5f;
            return Status.Success;
        }

    }
}