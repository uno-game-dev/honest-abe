//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a int value between minimum [inclusive] and maximun [exclusive].
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a int value between minimum [inclusive] and maximun [exclusive]",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Random.Range.html")]
    public class IntRandomRange : ActionNode {

        /// <summary>
        /// The minimum value.
        /// </summary>
        [VariableInfo(tooltip = "The minimum value")]
        public IntVar minimum;

        /// <summary>
        /// The maximun value.
        /// </summary>
        [VariableInfo(tooltip = "The maximun value")]
        public IntVar maximun;

        /// <summary>
        /// Store the random selected int.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected int")]
        public IntVar storeInt;

        public override void Reset () {
            minimum = new ConcreteIntVar();
            maximun = new ConcreteIntVar();
            storeInt = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (minimum.isNone || maximun.isNone || storeInt.isNone)
                return Status.Error;

            // Randomly selects an int
            storeInt.Value = Random.Range(minimum.Value, maximun.Value);
            return Status.Success;
        }

    }
}