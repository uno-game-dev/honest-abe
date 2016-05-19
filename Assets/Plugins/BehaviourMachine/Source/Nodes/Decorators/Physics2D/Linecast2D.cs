//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Tick the child if there is any collider intersecting the line between start and end.
    /// Returns Failure if there is no collider in the line.
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics2D/",
                icon = "Physics2D",
                description = "Tick the child if there is any collider intersecting the line between start and end. Returns Failure if there is no collider in the line",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics2D.Linecast.html")]
    public class Linecast2D : DecoratorNode {

    	/// <summary>
        /// The start point of the line in world space.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The start point of the line in world space")]
        public GameObjectVar startPoint;

        /// <summary>
        /// The end point of the line in world space.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The end point of the line in world space")]
        public GameObjectVar endPoint;

        /// <summary>
        /// Filter to detect Colliders only on certain layers.
        /// <summary>
        [Tooltip("Filter to detect Colliders only on certain layers")]
        public LayerMask layers;

        /// <summary>
        /// Stores the game object that was hit by the line.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the game object that was hit by the line")]
        public GameObjectVar storeGameObject;

        /// <summary>
        /// Stores the fraction of the distance along the line that the hit occurred.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the fraction of the distance along the line that the hit occurred")]
        public FloatVar storeFraction;

        /// <summary>
        /// Stores the impact point in world space where the line hit the collider.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the impact point in world space where the line hit the collider")]
        public Vector3Var storePoint;

        [System.NonSerialized]
        Transform m_StartTransform = null;
        [System.NonSerialized]
        Transform m_EndTransform = null;

        public override void Reset () {
            startPoint = new ConcreteGameObjectVar(this.self);
            endPoint = new ConcreteGameObjectVar(this.self);
            layers = 0;
            storeGameObject = new ConcreteGameObjectVar();
            storeFraction = new ConcreteFloatVar();
            storePoint = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Get the transform1
            if (m_StartTransform == null || m_StartTransform.gameObject != startPoint.Value)
                m_StartTransform = startPoint.Value != null ? startPoint.Value.transform : null;

            // Get the transform2
            if (m_EndTransform == null || m_EndTransform.gameObject != endPoint.Value)
                m_EndTransform = endPoint.Value != null ? endPoint.Value.transform : null;

            // Validate members
            if (m_StartTransform == null || m_EndTransform == null)
                return Status.Error;

            // Linecast
            RaycastHit2D hit = Physics2D.Linecast(m_StartTransform.position, m_EndTransform.position, layers);

            // Is there a hit in the line?
            if (hit.collider != null) {
                // Store result
                storeGameObject.Value = hit.collider.gameObject;
                storeFraction.Value = hit.fraction;
                storePoint.Value = hit.point;

                // Tick child
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
                storeFraction.Value = 0f;
                storePoint.Value = Vector3.zero;

                return Status.Failure;
            }
        }
    }
}
#endif