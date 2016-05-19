//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the player is touching a specific Collider.
    /// </summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Returns Success if the player is touching a specific Collider")]
    public class IsTouchingCollider : ConditionNode {

        /// <summary>
        /// The game object that has a Collider to test.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a Collider to test")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Only use the touch with this fingerId.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Any Id", tooltip = "Only use the touch with this fingerId")]
    	public IntVar fingerId;

        /// <summary>
        /// Only test touches in this phase.
        /// </summary>
        [Tooltip("Only test touches in this phase")]
        public TouchPhase touchPhase;

        /// <summary>
        /// The maximum distance from the camera to test.
        /// </summary>
        [VariableInfo(tooltip = "The maximum distance from the camera to test")]
        public FloatVar distance;

        /// <summary>
        /// A Layer mask that is used to selectively ignore colliders.
        /// </summary>
        [Tooltip("A Layer mask that is used to selectively ignore colliders")]
        public LayerMask layerMask;


        /// <summary>
        /// Store the fingerId of the touch, used only if the "Finger Id" parameter is Any Id.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the fingerId of the touch, used only if the \"Finger Id\" parameter is Any Id")]
        public IntVar storeFingerId;

        /// <summary>
        /// Store the position of the touch in screen coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the position of the touch in screen coordinates")]
        public Vector3Var storeTouchPos;

        /// <summary>
        /// Store the point that the touch hits the collider.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the point that the touch hits the collider")]
        public Vector3Var storePoint;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            fingerId = new ConcreteIntVar();
            touchPhase = TouchPhase.Began;
            distance = Mathf.Infinity;
            layerMask = -1;
            storeFingerId = new ConcreteIntVar();
            storeTouchPos = new ConcreteVector3Var();
            storePoint = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate Members
            if (distance.isNone || gameObject.Value == null)
                return Status.Error;

            // Is there at least one touch?
            if (Input.touchCount < 0)
                return Status.Failure;

            // Get the touches
            var touches = Input.touches;

            // Get the main camera
            Camera mainCamera = Camera.main;

            // Create a RaycastHit
            RaycastHit hit;

            // Any touch?
            if (fingerId.isNone) {
                for (int i = 0; i < touches.Length; i++) {
                    // Get the touch
                    var touch = touches[i];
                    // Test the touch phase and cast a ray in the scene
                    if (touch.phase == touchPhase && Physics.Raycast(mainCamera.ScreenPointToRay(touch.position), out hit, distance.Value, layerMask)) {
                        // Its the target game object?
                        if (hit.collider.gameObject == gameObject.Value) {
                            storeFingerId.Value = touch.fingerId;
                            storeTouchPos.Value = touch.position;
                            storePoint.Value = hit.point;

                            // Send event?
                            if (onSuccess.id != 0)
                               owner.root.SendEvent(onSuccess.id);

                            return Status.Success;
                        }
                    }
                }
            }
            else {
                for (int i = 0; i < touches.Length; i++) {
                    // Get the touch
                    var touch = touches[i];
                    // Test the touch id, the touch phase and cast a ray in the scene
                    if (touch.fingerId == fingerId.Value && touch.phase == touchPhase && Physics.Raycast(mainCamera.ScreenPointToRay(touch.position), out hit, distance.Value, layerMask)) {
                        // Its the target game object?
                        if (hit.collider.gameObject == gameObject.Value) {
                            storeTouchPos.Value = touch.position;
                            storePoint.Value = hit.point;

                            // Send event?
                            if (onSuccess.id != 0)
                               owner.root.SendEvent(onSuccess.id);

                            return Status.Success;
                        }
                    }
                }
            }

            return Status.Failure;
        }
    }
}