//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a value to a FloatVar.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Multiply \"Variable\" by \"Multiply By\"")]
    public class FloatAdd : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The FloatVar to change value")]
        public FloatVar variable;

        /// <summary>
        /// Add factor.
        /// </summary>
        [VariableInfo(tooltip = "Add factor")]
        public FloatVar addBy;

        /// <summary>
        /// If true the operation will be applied every second; otherwise the operation will be applied instantaneously
        /// </summary>
        [Tooltip("If true the operation will be applied every second; otherwise the operation will be applied instantaneously")]
        public bool perSecond = false;

        public override void Reset () {
            variable = new ConcreteFloatVar();
            addBy = new ConcreteFloatVar();
            perSecond = false;
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || addBy.isNone)
                return Status.Error;

            if (perSecond)
                variable.Value += addBy.Value * owner.deltaTime;
            else
                variable.Value += addBy.Value;

            return Status.Success;
        }
    }
}