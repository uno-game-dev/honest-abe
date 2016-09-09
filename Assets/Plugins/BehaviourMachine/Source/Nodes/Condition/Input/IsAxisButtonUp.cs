//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Same as IsButtonUp but uses an axis virtual button (e.g. Horizontal, Vertical). Returns Success the first frame the user releases the virtual button identified by Button Name.
    /// <summary>
    [NodeInfo ( category = "Condition/Input/",
                icon = "Button",
                description = "Same as IsButtonUp but uses an axis virtual button (e.g. Horizontal, Vertical). Returns Success the first frame the user releases the virtual button identified by Button Name")]
    public class IsAxisButtonUp : ConditionNode {

        public enum AxisValue {Positive, Negative}

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

        bool isButton = false;
        bool isButtonUp = false;

        void MyUpdate () {
            if (axisValue == AxisValue.Positive) {
                // Is down?
                if (isButton) {
                    if (Input.GetAxis(buttonName.Value) > tolerance.Value)
                        isButton = false;
                    this.isButtonUp = false;
                }
                else {
                    if (Input.GetAxis(buttonName.Value) <= tolerance.Value) {
                        isButton = true;

                        // Send event?
                        if (onSuccess.id != 0)
                            owner.root.SendEvent(onSuccess.id);

                        this.isButtonUp = true;
                    }
                    else
                        this.isButtonUp = false;
                }
            }
            else {
                // Is down?
                if (isButton) {
                    if (-Input.GetAxis(buttonName.Value) > tolerance.Value)
                        isButton = false;
                    this.isButtonUp = false;
                }
                else {
                    if (-Input.GetAxis(buttonName.Value) <= tolerance.Value) {
                        isButton = true;

                        // Send event?
                        if (onSuccess.id != 0)
                            owner.root.SendEvent(onSuccess.id);

                        this.isButtonUp = true;
                    }
                    else
                        this.isButtonUp = false;
                }
            }
        }

        public override void Reset () {
            base.Reset();

            buttonName = "Horizontal";
            tolerance = 0.3f;
        }

        public override void OnEnable () {
            InternalGlobalBlackboard.update += this.MyUpdate;
        }

        public override void OnDisable () {
            InternalGlobalBlackboard.update -= this.MyUpdate;
            isButton = false;
            isButtonUp = false;
        }

        public override Status Update () {
            // Validate members
            if (buttonName.isNone || tolerance.isNone)
                return Status.Error;

            if (this.isButtonUp) {
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