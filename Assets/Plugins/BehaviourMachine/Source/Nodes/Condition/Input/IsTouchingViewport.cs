//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the player is touching a viewport region on the screen; otherwise returns Failure.
    /// </summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Returns Success if the player is touching a viewport region on the screen; otherwise returns Failure")]
    public class IsTouchingViewport : ConditionNode {

        /// <summary>
        /// The rectangle of the viewport to test.
        /// The bottom-left of the camera is (0,0); the top-right is (1,1). The z position is in world units from the camera.
        /// </summary>
        [VariableInfo(tooltip = "The rectangle of the viewport to test. The bottom-left of the camera is (0,0); the top-right is (1,1). The z position is in world units from the camera")]
        public RectVar position;

        /// <summary>
        /// Only use the touch with this fingerId.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Any Id", tooltip = "Only use the touch with this fingerId")]
    	public IntVar fingerId;

        /// <summary>
        /// The phase of the touch.
        /// </summary>
        [Tooltip("The phase of the touch")]
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

            position = new ConcreteRectVar(new Rect(0f,0f,1f,1f));
            fingerId = new ConcreteIntVar();
            touchPhase = TouchPhase.Began;
            storeFingerId = new ConcreteIntVar();
            storeTouchPos = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate Members
            if (position.isNone)
                return Status.Error;

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
                    // Test the touch phase, and the viewport rect
                    if (touch.phase == touchPhase && position.Value.Contains(Camera.main.ScreenToViewportPoint(touch.position))) {
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
                    if (touch.fingerId == fingerId.Value && touch.phase == touchPhase && position.Value.Contains(Camera.main.ScreenToViewportPoint(touch.position))) {
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