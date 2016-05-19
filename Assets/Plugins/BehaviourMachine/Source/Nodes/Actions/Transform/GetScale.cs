//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the "Game Object" scale.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Gets the \"Game Object\" scale")]
    public class GetScale : ActionNode {

        /// <summary>
        /// The game object to get the scale.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the scale")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the scale.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the scale")]
        public Vector3Var storeScale;

        /// <summary>
        /// Store the scale.x.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the scale.x")]
        public FloatVar storeX;

        /// <summary>
        /// Store the scale.y.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the scale.y")]
        public FloatVar storeY;

        /// <summary>
        /// Store the scale.z.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the scale.z")]
        public FloatVar storeZ;

        /// <summary>
        /// Self, gets the scale relative to the parent's scale. World, gets the scale in world space.
        /// </summary>
        [Tooltip("Self, gets the scale relative to the parent's scale. World, gets the scale in world space")]
        public Space relativeTo = Space.Self;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            storeScale = new ConcreteVector3Var();
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

            // Store scale
            if (relativeTo == Space.Self)
                storeScale.Value = m_Transform.localScale;
            else
                storeScale.Value = m_Transform.lossyScale;

            storeX.Value = storeScale.Value.x;
            storeY.Value = storeScale.Value.y;
            storeZ.Value = storeScale.Value.z;

            return Status.Success;
        }
    }
}