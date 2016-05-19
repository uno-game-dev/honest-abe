//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Updates the variables and tick the child for each collider touching or inside the sphere.
    /// Returns Failure if there is no collider in the sphere.
    /// </summary>
    [NodeInfo ( category = "Decorator/Physics/",
                icon = "PlayLoopOff",
                description = "Updates the variables and tick the child for each collider touching or inside the sphere. Returns Failure if there is no collider in the sphere",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.OverlapSphere.html")]
    public class OverlapSphere : DecoratorNode {

    	/// <summary>
        /// The center position of the sphere in world space.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The center position of the sphere in world space")]
        public GameObjectVar center;

        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        [VariableInfo(tooltip = "The radius of the sphere")]
        public FloatVar radius;

        /// <summary>
        /// Filter to detect Colliders only on certain layers.
        /// <summary>
        [Tooltip("Filter to detect Colliders only on certain layers")]
        public LayerMask layers;

        /// <summary>
        /// Store the game object inside the sphere.
        /// <summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Store the game object inside the sphere")]
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

            // OverlapSphere
            Collider[] colliders = Physics.OverlapSphere(m_CenterTransform.position, radius.Value, layers);

            // Is there at least one collider inside the sphere?
            if (colliders.Length > 0) {
                storeGameObject.Value = colliders[0].gameObject;

                // Tick child?
                if (child != null) {
                    for (int i = 0; i < colliders.Length; i++) {
                        storeGameObject.Value = colliders[i].gameObject;
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
                return Status.Failure;
            }
        }
    }
}