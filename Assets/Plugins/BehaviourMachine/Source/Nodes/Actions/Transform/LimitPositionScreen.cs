//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Limit the position of a game object in the Main Camera View
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Limit the position of a game object in the Main Camera View")]
    public class LimitPositionScreen : ActionNode {

    	/// <summary>
        /// The game object to limit the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to limit the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Offset for x axis.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Offset for x axis")]
        public FloatVar offsetX;

        /// <summary>
        /// Offset for y axis.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Offset for y axis")]
        public FloatVar offsetY;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            offsetX = new ConcreteFloatVar();
            offsetY = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (m_Transform == null)
                return Status.Error;

            // Get main camera
            Camera mainCamera = Camera.main;

            // Get position
            var position = m_Transform.position;
            
            // Get bottom left point
            Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, position.z));
            // Get top right point
            Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, position.z));

            // Clamp position.x
            if (offsetX.isNone)
                position.x = Mathf.Clamp(position.x, bottomLeft.x, topRight.x);
            else {
                float offX = Mathf.Sign(position.x) * offsetX.Value;
                position.x = Mathf.Clamp(position.x, bottomLeft.x - offX, topRight.x - offX);
            }
            // Clamp position.y
            if (offsetY.isNone)
                position.y = Mathf.Clamp(position.y, bottomLeft.y, topRight.y);
            else {
                float offY = Mathf.Sign(position.y) * offsetY.Value;
                position.y = Mathf.Clamp(position.y, bottomLeft.y - offY, topRight.y - offY);
            }

            // Set new position
            m_Transform.position = position;

            return Status.Success;
        }
    }
}