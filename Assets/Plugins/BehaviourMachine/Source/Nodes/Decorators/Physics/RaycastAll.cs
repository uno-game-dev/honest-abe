//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Casts a ray against all colliders in the scene.
    /// Update the variables and tick the child for each collider that the ray hits.
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics/",
                icon = "PlayLoopOff",
                description = "Casts a ray against all colliders in the scene.Update the variables and tick the child for each collider that the ray hits",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.RaycastAll.html")]
    public class RaycastAll : DecoratorNode {

    	/// <summary>
        /// The starting point of the ray in world coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The starting point of the ray in world coordinates")]
        public GameObjectVar origin;

        /// <summary>
        /// The direction of the ray. The forward direction (blue axis) of the game object in world coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The direction of the ray. The forward direction (blue axis) of the game object in world coordinates")]
        public GameObjectVar direction;

        /// <summary>
        /// The length of the ray.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The length of the ray")]
        public FloatVar distance;

        /// <summary>
        /// Filter to detect Colliders only on certain layers.
        /// <summary>
        [Tooltip("Filter to detect Colliders only on certain layers")]
        public LayerMask layers;

        /// <summary>
        /// Stores True if the Raycast hits some collider; stores False otherwise.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores True if the Raycast hits some collider; stores False otherwise")]
        public BoolVar storeResult;

        /// <summary>
        /// Stores the game object that was hit by the ray.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the game object that was hit by the ray")]
        public GameObjectVar storeGameObject;

        /// <summary>
        /// Stores the distance from the ray's origin to the impact point.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the distance from the ray's origin to the impact point")]
        public FloatVar storeDistance;

        /// <summary>
        /// Stores the impact point in world space where the ray hit the collider.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the impact point in world space where the ray hit the collider")]
        public Vector3Var storePoint;

        [System.NonSerialized]
        Transform m_OriginTransform = null;
        [System.NonSerialized]
        Transform m_DirectionTransform = null;

        public override void Reset () {
            origin = new ConcreteGameObjectVar(this.self);
            direction = new ConcreteGameObjectVar(this.self);
            distance = new ConcreteFloatVar();
            layers = 0;
            storeResult = new ConcreteBoolVar();
            storeGameObject = new ConcreteGameObjectVar();
            storeDistance = new ConcreteFloatVar();
            storePoint = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Get the origin transform
            if (m_OriginTransform == null || m_OriginTransform.gameObject != origin.Value)
                m_OriginTransform = origin.Value != null ? origin.Value.transform : null;

            // Get the direction
            if (m_DirectionTransform == null || m_DirectionTransform.gameObject != direction.Value)
                m_DirectionTransform = direction.Value != null ? direction.Value.transform : null;

            // Validate members
            if (m_OriginTransform == null || m_DirectionTransform == null)
                return Status.Error;

            // Raycast all
            RaycastHit[] hits = Physics.RaycastAll(m_OriginTransform.position, m_DirectionTransform.forward, !distance.isNone ? distance.Value : Mathf.Infinity, layers);

            // Is there at least one hit in the ray?
            if (hits.Length > 0) {
                // Stores result
                storeResult.Value = true;
                storeGameObject.Value = hits[0].collider.gameObject;
                storeDistance.Value = hits[0].distance;
                storePoint.Value = hits[0].point;

                // Tick child?
                if (child != null) {
                    for (int i = 0; i < hits.Length; i++) {
                        storeGameObject.Value = hits[i].collider.gameObject;
                        storeDistance.Value = hits[i].distance;
                        storePoint.Value = hits[i].point;
                        child.OnTick();

                        // Break?
                        if (child.status == Status.Error || child.status == Status.Running)
                            break;
                    }

                    // Set status
                    return child.status;
                }
                else
                    return Status.Success;
            }
            else {
                storeResult.Value = false;
                storeGameObject.Value = null;
                storeDistance.Value = 0f;
                storePoint.Value = Vector3.zero;

                return Status.Failure;
            }
        }
    }
}