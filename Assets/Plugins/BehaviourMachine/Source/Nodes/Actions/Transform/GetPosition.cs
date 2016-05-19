//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the "Game Object" position.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Gets the \"Game Object\" position")]
    public class GetPosition : ActionNode {

        /// <summary>
        /// The game object to get the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the position.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the position")]
        public Vector3Var storePosition;

        /// <summary>
        /// Store the position.x.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the position.x")]
        public FloatVar storeX;

        /// <summary>
        /// Store the position.y.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the position.y")]
        public FloatVar storeY;

        /// <summary>
        /// Store the position.z.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the position.z")]
        public FloatVar storeZ;

        /// <summary>
        /// Self, gets the position relative to the parent's position. World, gets the position in world space.
        /// </summary>
        [Tooltip("Self, gets the position relative to the parent's position. World, gets the position in world space")]
        public Space relativeTo = Space.Self;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            storePosition = new ConcreteVector3Var();
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

            // Get the position
            Vector3 position = relativeTo == Space.Self ? m_Transform.localPosition : m_Transform.position;

            // Store the position
            storePosition.Value = position;
            storeX.Value = position.x;
            storeY.Value = position.y;
            storeZ.Value = position.z;

            return Status.Success;
        }
    }
}