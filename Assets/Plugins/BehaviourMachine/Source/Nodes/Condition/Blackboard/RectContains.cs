//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if the "Rect" contains the "Point"; otherwise returns Failure.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if the \"Rect\" contains the \"Point\"; otherwise returns Failure")]
    public class RectContains : ConditionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The rect to test")]
        public RectVar rect;

        /// <summary>
        /// The new variable value.
        /// </summary>
        [VariableInfo(tooltip = "The point")]
        public Vector3Var point;

        [VariableInfo(tooltip = "Overrides \"Point.x\"")]
        public FloatVar x;

        [VariableInfo(tooltip = "Overrides \"Point.y\"")]
        public FloatVar y;

        public override void Reset () {
            base.Reset();

            rect = new ConcreteRectVar();
            point = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (rect.isNone)
                return Status.Error;

            // Get point
            var desiredPoint = point.isNone ? Vector3.zero : point.Value;

            // Override x or y?
            if (!x.isNone) desiredPoint.x = x.Value;
            if (!y.isNone) desiredPoint.y = y.Value;

            // Contains point?
            if (rect.Value.Contains(desiredPoint)) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }

    }
}