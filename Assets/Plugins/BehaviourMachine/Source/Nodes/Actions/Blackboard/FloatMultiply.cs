//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Multiply "Variable" by "Multiply By".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Multiply \"Variable\" by \"Multiply By\"")]
    public class FloatMultiply : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The FloatVar to change value")]
        public FloatVar variable;

        /// <summary>
        /// Multiply factor.
        /// </summary>
        [VariableInfo(tooltip = "Multiply factor")]
        public FloatVar multiplyBy;

        /// <summary>
        /// If true the operation will be applied every second; otherwise the operation will be applied instantaneously
        /// </summary>
        [Tooltip("If true the operation will be applied every second; otherwise the operation will be applied instantaneously")]
        public bool perSecond = false;

        public override void Reset () {
            variable = new ConcreteFloatVar();
            multiplyBy = new ConcreteFloatVar();
            perSecond = false;
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || multiplyBy.isNone)
                return Status.Error;

            if (perSecond)
                variable.Value *= multiplyBy.Value * owner.deltaTime;
            else
                variable.Value *= multiplyBy.Value;

            return Status.Success;
        }
    }
}