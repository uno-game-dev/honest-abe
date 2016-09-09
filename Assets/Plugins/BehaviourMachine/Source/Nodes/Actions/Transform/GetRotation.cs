//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the "Game Object" rotation or euler angles.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Gets the \"Game Object\" rotation or euler angles")]
    public class GetRotation : ActionNode {

        /// <summary>
        /// The game object to get the rotation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the rotation")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the rotation.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the rotation")]
        public QuaternionVar storeRotation;

        /// <summary>
        /// Store the rotation as euler angles.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the rotation as euler angles")]
        public Vector3Var storeEulerAngles;

        /// <summary>
        /// Store eulerAngles.x.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store eulerAngles.x")]
        public FloatVar storeX;

        /// <summary>
        /// Store eulerAngles.y.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store eulerAngles.y")]
        public FloatVar storeY;

        /// <summary>
        /// Store eulerAngles.z.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store eulerAngles.z")]
        public FloatVar storeZ;

        /// <summary>
        /// Self, gets the rotation relative to the parent's rotation. World, gets the rotation in world space.
        /// </summary>
        [Tooltip("Self, gets the rotation relative to the parent's rotation. World, gets the rotation in world space")]
        public Space relativeTo = Space.Self;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            storeRotation = new ConcreteQuaternionVar();
            storeEulerAngles = new ConcreteVector3Var();
            storeX = new ConcreteFloatVar();
            storeY = new ConcreteFloatVar();
            storeZ = new ConcreteFloatVar();
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (m_Transform == null)
                return Status.Error;

            // Local rotation?
            if (relativeTo == Space.Self) {
                // Store rotation?
                storeRotation.Value = m_Transform.localRotation;
                // Store eulerAngles?
                storeEulerAngles.Value = m_Transform.localEulerAngles;
            }
            else {
                // Store rotation?
                storeRotation.Value = m_Transform.rotation;
                // Store eulerAngles?
                storeEulerAngles.Value = m_Transform.eulerAngles;
            }

            storeX.Value = storeEulerAngles.Value.x;
            storeY.Value = storeEulerAngles.Value.y;
            storeZ.Value = storeEulerAngles.Value.z;

            return Status.Success;
        }
    }
}