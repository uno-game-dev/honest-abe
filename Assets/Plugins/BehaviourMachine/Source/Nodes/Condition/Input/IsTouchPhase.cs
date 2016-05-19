//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// The touch that has the "Finger Id" is in the "Phase"?.
    /// </summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "The touch that has the \"Finger Id\" is in the \"Phase\"?")]
    public class IsTouchPhase : ConditionNode {

        /// <summary>
        /// Only use the touch with this fingerId.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Any Id", tooltip = "Only use the touch with this fingerId")]
    	public IntVar fingerId;

        /// <summary>
        /// The touch phase to test.
        /// </summary>
        [Tooltip("The touch phase to test")]
        public TouchPhase touchPhase;

        /// <summary>
        /// Store the fingerId of the touch, used only if the "Finger Id" parameter is Any Id.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the fingerId of the touch, used only if the \"Finger Id\" parameter is Any")]
        public IntVar storeFingerId;

        /// <summary>
        /// Store the position of the touch in screen coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the position of the touch in screen coordinates")]
        public Vector3Var storeTouchPos;

        public override void Reset () {
            base.Reset();

            fingerId = new ConcreteIntVar();
            touchPhase = TouchPhase.Began;
            storeFingerId = new ConcreteIntVar();
            storeTouchPos = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Is there at least one touch?
            if (Input.touchCount < 0)
                return Status.Failure;

            // Get the touches
            var touches = Input.touches;

            // Any touch?
            if (fingerId.isNone) {
                for (int i = 0; i < touches.Length; i++) {
                    // Get the touch
                    var touch = touches[i];
                    // Test the touch phase
                    if (touch.phase == touchPhase) {
                        storeFingerId.Value = touch.fingerId;
                        storeTouchPos.Value = touch.position;

                        // Send event?
                        if (onSuccess.id != 0)
                            owner.root.SendEvent(onSuccess.id);

                        return Status.Success;
                    }
                }
            }
            else {
                for (int i = 0; i < touches.Length; i++) {
                    // Get the touch
                    var touch = touches[i];
                    // Test the fingerId, the touch phase, and the viewport rect
                    if (touch.fingerId == fingerId.Value && touch.phase == touchPhase) {
                        storeTouchPos.Value = touch.position;

                        // Send event?
                        if (onSuccess.id != 0)
                            owner.root.SendEvent(onSuccess.id);

                        return Status.Success;
                    }
                }
            }

            return Status.Failure;
        }
    }
}