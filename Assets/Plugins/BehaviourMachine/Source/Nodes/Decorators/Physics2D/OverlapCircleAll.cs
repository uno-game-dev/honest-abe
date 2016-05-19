//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// For each collider that falls within a circular area the child is ticked.
    /// Returns Failure if there is no collider inside the circular area.
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics2D/",
                icon = "Physics2D",
                description = "For each collider that falls within a circular area the child is ticked. Returns Failure if there is no collider inside the circular area. ",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics2D.OverlapCircleAll.html")]
    public class OverlapCircleAll : DecoratorNode {

    	/// <summary>
        /// The central position of the circle in world space
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The central position of the circle in world space")]
        public GameObjectVar center;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        [VariableInfo(tooltip = "The radius of the circle")]
        public FloatVar radius;

        /// <summary>
        /// Filter to detect Colliders only on certain layers.
        /// <summary>
        [Tooltip("Filter to detect Colliders only on certain layers")]
        public LayerMask layers;

        /// <summary>
        /// Stores the game object inside the circle.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the game object inside the circle")]
        public GameObjectVar storeGameObject;

        [System.NonSerialized]
        Transform m_CenterTransform = null;

        public override void Reset () {
            center = new ConcreteGameObjectVar(this.self);
            radius = new ConcreteFloatVar();
            layers = 0;
            storeGameObject = new ConcreteGameObjectVar();
        }

        public override Status Update () {
            // Get the transform1
            if (m_CenterTransform == null || m_CenterTransform.gameObject != center.Value)
                m_CenterTransform = center.Value != null ? center.Value.transform : null;

            // Validate members
            if (m_CenterTransform == null || radius.isNone)
                return Status.Error;

            // OverlapCircleAll
            Collider2D[] colliders2D = Physics2D.OverlapCircleAll(m_CenterTransform.position, radius.Value, layers);

            // Is there at least one collider2D inside the circle?
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

                    // Set status
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