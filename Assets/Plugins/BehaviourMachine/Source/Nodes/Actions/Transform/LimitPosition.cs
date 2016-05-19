//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Limit the position of a game object.
    /// Uses world coordinates.
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Limit the position of a game object. Uses world coordinates",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Translate.html")]
    public class LimitPosition : ActionNode {

        /// <summary>
        /// The game object to limit the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to limit the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The minimum x coordinates the "Game Object" can have.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "-Infinity", tooltip = "The minimum x coordinates the \"Game Object\" can have")]
        public FloatVar minX;

        /// <summary>
        /// The minimum y coordinates the "Game Object" can have.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "-Infinity", tooltip = "The minimum y coordinates the \"Game Object\" can have")]
        public FloatVar minY;

        /// <summary>
        /// The minimum z coordinates the "Game Object" can have.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "-Infinity", tooltip = "The minimum z coordinates the \"Game Object\" can have")]
        public FloatVar minZ;

        /// <summary>
        /// The maximum x coordinates the "Game Object" can have.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The maximum x coordinates the \"Game Object\" can have")]
        public FloatVar maxX;

        /// <summary>
        /// The maximum y coordinates the "Game Object" can have.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The maximum y coordinates the \"Game Object\" can have")]
        public FloatVar maxY;

        /// <summary>
        /// The maximum z coordinates the "Game Object" can have.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The maximum z coordinates the \"Game Object\" can have")]
        public FloatVar maxZ;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            minX = new ConcreteFloatVar();
            minY = new ConcreteFloatVar();
            minZ = new ConcreteFloatVar();
            maxX = new ConcreteFloatVar();
            maxY = new ConcreteFloatVar();
            maxZ = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (m_Transform == null)
                return Status.Error;

            // Clamp position
            var position = m_Transform.position;
            position.x = Mathf.Clamp(position.x, !minX.isNone ? minX.Value : Mathf.NegativeInfinity, !maxX.isNone ? maxX.Value : Mathf.Infinity);
            position.y = Mathf.Clamp(position.y, !minY.isNone ? minY.Value : Mathf.NegativeInfinity, !maxY.isNone ? maxY.Value : Mathf.Infinity);
            position.z = Mathf.Clamp(position.z, !minZ.isNone ? minZ.Value : Mathf.NegativeInfinity, !maxZ.isNone ? maxZ.Value : Mathf.Infinity);

            // Set new position
            m_Transform.position = position;

            return Status.Success;
        }
    }
}