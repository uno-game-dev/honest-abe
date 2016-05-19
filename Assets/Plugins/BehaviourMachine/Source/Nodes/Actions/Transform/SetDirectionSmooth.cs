//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" direction over time.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets the \"Game Object\" direction over time")]
    public class SetDirectionSmooth : ActionNode {

        /// <summary>
        /// The game object to set the direction.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the direction")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new direction.
        /// </summary>
        [VariableInfo(tooltip = "The new direction")]
        public Vector3Var newDirection;

        /// <summary>
        /// The minimum sqrMagnitude of the 'Target Direction' to apply the rotation.
        /// </summary>
        [VariableInfo(tooltip = "The minimum sqrMagnitude of the \'Target Direction\' to apply the rotation")]
        public FloatVar minSqrMagnitude;

        /// <summary>
        /// The up direction.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Up (green axis)", tooltip = "The up direction")]
        public Vector3Var upDirection;

        /// <summary>
        /// The angular velocity.
        /// </summary>
        [VariableInfo(tooltip = "The angular velocity")]
        public FloatVar angularVelocity;

        /// <summary>
        /// Should ignore the y axis?
        /// </summary>
        [VariableInfo(tooltip = "Should ignore the y axis?")]
        public BoolVar ignoreYAxis;

        [System.NonSerialized]
        Transform m_Transform = null;
        [System.NonSerialized]
        Quaternion m_TargetDirection;
        [System.NonSerialized]
        Quaternion m_SmoothDirection;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newDirection = new ConcreteVector3Var();
            upDirection = new ConcreteVector3Var();
            minSqrMagnitude = 0.1f;
            angularVelocity = 6f;
            ignoreYAxis = true;
        }

        public override Status Update () {
            // The target transform has changed?
            if (m_Transform != gameObject.transform) {
                m_Transform = gameObject.transform;
                m_SmoothDirection = Quaternion.identity;
                m_TargetDirection = Quaternion.identity;
            }

            // Validate members
            if  (m_Transform == null || newDirection.isNone || angularVelocity.isNone)
                return Status.Error;

            // Get the direction
            Vector3 direction = newDirection.Value;

            // Ignore y axis?
            if (ignoreYAxis.Value)
                direction.y = 0f;

            // The direction sqrMagnitude is greater than the minSqrMagnitude?
            if (direction.sqrMagnitude > minSqrMagnitude.Value) {
                // Get the desired rotation
                m_TargetDirection = Quaternion.LookRotation(direction, !upDirection.isNone ? upDirection.Value : Vector3.up);
            }   

            // Get the smooth rotation
            m_SmoothDirection = Quaternion.Slerp(m_SmoothDirection, m_TargetDirection, angularVelocity.Value * owner.deltaTime);
            // Rotate
            m_Transform.rotation = m_SmoothDirection;

            return Status.Success;
        }
    }
}