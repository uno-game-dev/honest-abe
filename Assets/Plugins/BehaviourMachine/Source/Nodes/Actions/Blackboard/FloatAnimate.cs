//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Animates a float variable.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Animates a float variable")]
    public class FloatAnimate : ActionNode {

    	/// <summary>
        /// The FloatVar to animate.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The FloatVar to animate")]
        public FloatVar variable;

        [Tooltip("The curve to animate the float variable")]
        public AnimationCurve curve;

        /// <summary>
        /// Optional value to store the timer of the animation. You can reset the animation by setting this value to zero.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Optional value to store the timer of the animation. You can reset the animation by setting this value to zero")]
        public FloatVar timer;

        public override void Reset () {
            variable = new ConcreteFloatVar();
            curve = new AnimationCurve();
            timer = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            // Update timer
            timer.Value += owner.deltaTime;

            variable.Value = curve.Evaluate(timer.Value);

            return Status.Success;
        }
    }
}