//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The axis value.
    /// Used by BehaviourMachine.InputUtility.
    /// </summary>
    public enum AxisValue {Positive, Negative}

    /// <summary>
    /// Same as IsButtonDown but uses an axis virtual button (e.g. Horizontal, Vertical). Returns Success while the virtual button identified by Button Name is held down.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Button",
                description = "Same as IsButtonDown but uses an axis virtual button (e.g. Horizontal, Vertical). Returns Success while the virtual button identified by Button Name is held down")]
    public class IsAxisButton : ConditionNode {

        /// <summary>
        /// The virtual axis button to test.
        /// <summary>
        [VariableInfo(tooltip = "The virtual axis button to test")]
        public StringVar buttonName;

        /// <summary>
        /// Positive: Use positive value of the axis.
        /// Negative: Use negative value of the axis.
        /// <summary>
        [Tooltip("Positive: Use positive value of the axis.\nNegative: Use negative value of the axis")]
        public AxisValue axisValue;

        /// <summary>
        /// The tolerance value.
        /// </summary>
        [Range(0,1)]
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The tolerance value")]
        public FloatVar tolerance;

        /// <summary>
        /// Returns Success while the virtual axis button is held down.
        /// <param name="axisName">The name of the axis.</param>
        /// <param name="axisValue">The name of the axis.</param>
        /// <param name="tolerance">The name of the axis.</param>
        /// </summary>
        public static bool IsAxisButtonTest (string axisName, AxisValue axisValue, float tolerance) {
            if (axisValue == AxisValue.Positive)
                return Input.GetAxisRaw(axisName) >= tolerance;
            else
                return -Input.GetAxisRaw(axisName) >= tolerance;
        }

        public override void Reset () {
            base.Reset();

            buttonName = "Horizontal";
            tolerance = 0.3f;
        }

        public override Status Update () {
            if (IsAxisButton.IsAxisButtonTest(buttonName.Value, axisValue, tolerance.Value)) {
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