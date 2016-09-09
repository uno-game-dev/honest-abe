//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Remap a float value.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Remap a float value")]
    public class FloatRemap : ActionNode {

        /// <summary>
        /// The float value to be remaped.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The float value to be remaped")]
        public FloatVar In;

        /// <summary>
        /// the minimum value of In.
        /// </summary>
        [VariableInfo(tooltip = "the minimum value of \'In\'")]
        public FloatVar inMin;

        /// <summary>
        /// the maximum value of In.
        /// </summary>
        [VariableInfo(tooltip = "the maximum value of \'In\'")]
        public FloatVar inMax;

        /// <summary>
        /// the minimum value of Out Max.
        /// </summary>
        [VariableInfo(tooltip = "the minimum value of \'Out Max\'")]
        public FloatVar outMin;

        /// <summary>
        /// the maximum value of Out Max.
        /// </summary>
        [VariableInfo(tooltip = "the maximum value of \'Out Max\'")]
        public FloatVar outMax;

        /// <summary>
        /// Store the remapped value of 'In'.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the remapped value of \'In\'")]
        public FloatVar storeOut;

        public override void Reset () {
            In = new ConcreteFloatVar();
            inMin = 0f;
            inMax = 1023f;
            outMin = 0f;
            outMax = 1f;
            storeOut = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (In.isNone || storeOut.isNone)
                return Status.Error;

            // Randomly selects a float
            storeOut.Value = (In.Value - inMin.Value) / (inMax.Value - inMin.Value) * (outMax.Value - outMin.Value) + outMin.Value;
            return Status.Success;
        }

    }
}