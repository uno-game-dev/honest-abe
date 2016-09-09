//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of ints.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a value in a set of ints")]
    public class IntRandom : ActionNode {

        /// <summary>
        /// The possible int values.
        /// </summary>
        [VariableInfo(tooltip = "The possible int values")]
        public IntVar[] ints;

        /// <summary>
        /// Store the random selected int.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected int")]
        public IntVar storeInt;

        public override void Reset () {
            ints = new IntVar[0];
            storeInt = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (ints.Length == 0 || storeInt.isNone)
                return Status.Error;

            // Randomly selects an int
            storeInt.Value = ints[Random.Range(0, ints.Length)].Value;
            return Status.Success;
        }

    }
}