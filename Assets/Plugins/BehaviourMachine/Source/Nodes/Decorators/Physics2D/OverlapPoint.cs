//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// If a collider overlaps a point in the space the child is ticked.
    /// Returns Failure if there is no collider that overlaps the point.
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics2D/",
                icon = "Physics2D",
                description = "If a collider overlaps a point in the space the child is ticked. Returns Failure if there is no collider that overlaps the point",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics2D.OverlapPoint.html")]
    public class OverlapPoint : DecoratorNode {

    	/// <summary>
        /// A point in world space.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A point in world space")]
        public GameObjectVar point;

        /// <summary>
        /// Filter to detect Colliders only on certain layers.
        /// <summary>
        [Tooltip("Filter to detect Colliders only on certain layers")]
        public LayerMask layers;

        /// <summary>
        /// Stores the game object inside the rectangle.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the game object inside the rectangle")]
        public GameObjectVar storeGameObject;

        [System.NonSerialized]
        Transform m_PointTransform = null;

        public override void Reset () {
            point = new ConcreteGameObjectVar(this.self);
            layers = 0;
            storeGameObject = new ConcreteGameObjectVar();
        }

        public override Status Update () {
            // Get the point transform
            if (m_PointTransform == null || m_PointTransform.gameObject != point.Value)
                m_PointTransform = point.Value != null ? point.Value.transform : null;

            // Validate members
            if (m_PointTransform == null)
                return Status.Error;

            // OverlapPoint
            Collider2D collider2D = Physics2D.OverlapPoint(m_PointTransform.position, layers);

            // Is there a collider2D inside the rectangle?
            if (collider2D != null) {
                // Store result
                storeGameObject.Value = collider2D.gameObject;

                // Tick child?
                if (child != null) {
                    child.OnTick();
                    return child.status;
                }
                else
                    return Status.Success;
            }
            else {
                // Store result
                storeGameObject.Value = null;
                return Status.Failure;
            }
        }
    }
}
#endif