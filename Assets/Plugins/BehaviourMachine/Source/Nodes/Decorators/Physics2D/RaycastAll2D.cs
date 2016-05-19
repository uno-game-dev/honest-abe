//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Casts a ray against all colliders2D in the scene.
    /// For each collider that the ray hits the child is ticked.
    /// Returns Failure if the ray does not hit any collider.
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics2D/",
                icon = "Physics2D",
                description = "Casts a ray against all colliders2D in the scene. For each collider that the ray hits the child is ticked. Returns Failure if the ray does not hit any collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics2D.RaycastAll.html")]
    public class RaycastAll2D : DecoratorNode {

    	/// <summary>
        /// The starting point of the ray in world coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The starting point of the ray in world coordinates")]
        public GameObjectVar origin;

        /// <summary>
        /// The direction of the ray. The right direction (red axis) of the game object in world coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The direction of the ray. The right direction (red axis) of the game object in world coordinates")]
        public GameObjectVar direction;

        /// <summary>
        /// Multiply the direction by the sign of the transform.localScale.x of the direction?.
        /// </summary>
        [Tooltip("Multiply the direction by the sign of the transform.localScale.x of the direction?")]
        public bool useScaleXSign = true;

        /// <summary>
        /// The length of the ray.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The length of the ray")]
        public FloatVar distance;

        /// <summary>
        /// Filter to detect Colliders2D only on certain layers.
        /// <summary>
        [Tooltip("Filter to detect Colliders2D only on certain layers")]
        public LayerMask layers;

        /// <summary>
        /// Stores the game object that was hit by the ray.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the game object that was hit by the ray")]
        public GameObjectVar storeGameObject;

        /// <summary>
        /// Stores the fraction of the distance along the ray that the hit occurred.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the fraction of the distance along the ray that the hit occurred")]
        public FloatVar storeFraction;

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
            useScaleXSign = true;
            distance = new ConcreteFloatVar();
            layers = 0;
            storeGameObject = new ConcreteGameObjectVar();
            storeFraction = new ConcreteFloatVar();
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
            RaycastHit2D[] hits = Physics2D.RaycastAll(m_OriginTransform.position, useScaleXSign ? Mathf.Sign(m_DirectionTransform.localScale.x) * m_DirectionTransform.right : m_DirectionTransform.right, !distance.isNone ? distance.Value : Mathf.Infinity, layers);

            // Is there at least one hit in the ray?
            if (hits.Length > 0) {
                // Store result
                storeGameObject.Value = hits[0].collider.gameObject;
                storeFraction.Value = hits[0].fraction;
                storePoint.Value = hits[0].point;

                // Tick child
                if (child != null) {
                    for (int i = 0; i < hits.Length; i++) {
                        storeGameObject.Value = hits[i].collider.gameObject;
                        storeFraction.Value = hits[i].fraction;
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
                // Store result
                storeGameObject.Value = null;
                storeFraction.Value = 0f;
                storePoint.Value = Vector3.zero;

                return Status.Failure;
            }
        }
    }
}
#endif