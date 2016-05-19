//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// For each collider that falls within a rectangular area the child is ticked.
    /// Returns Failure if there is no collider inside the rectangular area.
    /// The rectangle is defined by two diagonally opposite corners ("Point A", "Point B").
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics2D/",
                icon = "Physics2D",
                description = "For each collider that falls within a rectangular area the child is ticked. Returns Failure if there is no collider inside the rectangular area. The rectangle is defined by two diagonally opposite corners (\"Point A\", \"Point B\")",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics2D.OverlapAreaAll.html")]
    public class OverlapAreaAll : DecoratorNode {

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
            if (m_TransformA == null || m_TransformB)
                return Status.Error;

            // OverlapAreaAll
            Collider2D[] colliders2D = Physics2D.OverlapAreaAll(m_TransformA.position, m_TransformB.position, layers);

            // Is there at least one collider2D inside the rectangle?
            if (colliders2D.Length > 0) {
                // Store result
                storeGameObject.Value = colliders2D[0].gameObject;

                // Tick child?
                if (child != null) {
                    for (int i = 0; i < colliders2D.Length; i++) {
                        storeGameObject.Value = colliders2D[i].gameObject;
                        child.OnTick();

                        // Break?
                        if (child.status == Status.Error || child.status == Status.Running)
                            break;
                    }

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