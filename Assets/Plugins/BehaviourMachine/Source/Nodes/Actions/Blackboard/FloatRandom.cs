//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of floats.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a value in a set of floats")]
    public class FloatRandom : ActionNode {

        /// <summary>
        /// The possible float values.
        /// </summary>
        [VariableInfo(tooltip = "The possible float values")]
        public FloatVar[] floats;

        /// <summary>
        /// Store the random selected float.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected float")]
        public FloatVar storeFloat;

        public override void Reset () {
            floats = new FloatVar[0];
            storeFloat = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (floats.Length == 0 || storeFloat.isNone)
                return Status.Error;

            // Randomly selects a float
            storeFloat.Value = floats[Random.Range(0, floats.Length)].Value;
            return Status.Success;
        }

    }
}