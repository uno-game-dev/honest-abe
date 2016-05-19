//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Clamps "Value" between "Min" and "Max".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Clamps \"Value\" between \"Min\" and \"Max\"")]
    public class FloatClamp : ActionNode {

        /// <summary>
        /// The value to be clamped.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The value to be clamped")]
        public FloatVar value;

    	/// <summary>
        /// The minimum value.
        /// </summary>
        [VariableInfo(tooltip = "The minimum value")]
        public FloatVar min;

        /// <summary>
        /// The maximum value.
        /// </summary>
        [VariableInfo(tooltip = "The maximum value")]
        public FloatVar max;

        public override void Reset () {
            value = new ConcreteFloatVar();
            min = new ConcreteFloatVar();
            max = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (value.isNone || min.isNone || max.isNone)
                return Status.Error;

            value.Value = Mathf.Clamp(value.Value, min.Value, max.Value);

            return Status.Success;
        }
    }
}