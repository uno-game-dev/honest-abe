//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a float value between min [inclusive] and max [inclusive].
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a float value between min [inclusive] and max [inclusive]",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Random.Range.html")]
    public class FloatRandomRange : ActionNode {

        /// <summary>
        /// The minimum value.
        /// </summary>
        [VariableInfo(tooltip = "The minimum value")]
        public FloatVar minimum;

        /// <summary>
        /// The maximun value.
        /// </summary>
        [VariableInfo(tooltip = "The maximun value")]
        public FloatVar maximun;

        /// <summary>
        /// Store the random selected float.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected float")]
        public FloatVar storeFloat;

        public override void Reset () {
            minimum = new ConcreteFloatVar();
            maximun = new ConcreteFloatVar();
            storeFloat = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (minimum.isNone || maximun.isNone || storeFloat.isNone)
                return Status.Error;

            // Randomly selects a float
            storeFloat.Value = Random.Range(minimum.Value, maximun.Value);
            return Status.Success;
        }

    }
}