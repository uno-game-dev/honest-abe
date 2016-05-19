//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// The mouse is hovering a specifc collider?
    /// </summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "Mouse",
                description = "The mouse is hovering a specifc collider?")]
    public class IsMouseOverCollider : ConditionNode {

        /// <summary>
        /// The game object that has a Collider to test.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a Collider to test")]
        public GameObjectVar gameObject;

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
        /// Store the point that the ray hits the collider.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the point that the ray hits the collider")]
        public Vector3Var storePoint;

        public override void Reset () {
            base.Reset ();
            
            gameObject = new ConcreteGameObjectVar(this.self);
            distance = Mathf.Infinity;
            layerMask = -1;
            storePoint = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate Members
            if (distance.isNone)
                return Status.Error;

            // Get the main camera
            Camera mainCamera = Camera.main;

            // Create a RaycastHit
            RaycastHit hit;

            // Cast the ray
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, distance.Value, layerMask) && hit.collider.gameObject == gameObject.Value) {
                storePoint.Value = hit.point;

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