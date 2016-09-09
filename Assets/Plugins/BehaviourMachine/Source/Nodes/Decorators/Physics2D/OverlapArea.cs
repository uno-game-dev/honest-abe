//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// If a collider falls within a rectangular area the child is ticked.
    /// Returns Failure if there is no collider inside the rectangular area.
    /// The rectangle is defined by two diagonally opposite corner ("Point A", "Point B").
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics2D/",
                icon = "Physics2D",
                description = "If a collider falls within a rectangular area the child is ticked. Returns Failure if there is no collider inside the rectangular area. The rectangle is defined by two diagonally opposite corner (\"Point A\", \"Point B\")",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics2D.OverlapArea.html")]
    public class OverlapArea : DecoratorNode {

    	/// <summary>
        /// One corner of the rectangle.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "One corner of the rectangle")]
        public GameObjectVar pointA;

        /// <summary>
        /// Diagonally opposite corner of the rectangle.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "Diagonally opposite corner of the rectangle")]
        public GameObjectVar pointB;

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
        Transform m_TransformA = null;

        [System.NonSerialized]
        Transform m_TransformB = null;

        public override void Reset () {
            pointA = new ConcreteGameObjectVar(this.self);
            pointB = new ConcreteGameObjectVar(this.self);
            layers = 0;
            storeGameObject = new ConcreteGameObjectVar();
        }

        public override Status Update () {
            // Get the pointA transform
            if (m_TransformA == null || m_TransformA.gameObject != pointA.Value)
                m_TransformA = pointA.Value != null ? pointA.Value.transform : null;

            // Get the pointA transform
            if (m_TransformB == null || m_TransformB.gameObject != pointB.Value)
                m_TransformB = pointB.Value != null ? pointB.Value.transform : null;

            // Validate members
            if (m_TransformA == null || m_TransformB || child == null)
                return Status.Error;

            // OverlapArea
            Collider2D collider2D = Physics2D.OverlapArea(m_TransformA.position, m_TransformB.position, layers);

            // Is there a collider2D inside the rectangle?
            if (collider2D != null) {
                // Store result
                storeGameObject.Value = collider2D.gameObject;

                // Tick child?
                if (child != null) {
                    child.OnTick();
                    return child.status;
                }
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