//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// The mouse is over a collider?
    /// </summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "Mouse",
                description = "The mouse is over a collider?")]
    public class IsMouseOverAnyCollider : ConditionNode {

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
        /// Stores the GameObject behind the mouse.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the GameObject behind the mouse")]
        public GameObjectVar storeGameObject;

        /// <summary>
        /// Store the point that the ray hits the collider.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the point that the ray hits the collider")]
        public Vector3Var storePoint;

        public override void Reset () {
            base.Reset ();
            
            distance = Mathf.Infinity;
            layerMask = -1;
            storeGameObject = new ConcreteGameObjectVar();
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
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, distance.Value, layerMask)) {
                storeGameObject.Value = hit.collider.gameObject;
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