//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the color of the "Game Object".
    /// </summary>
    [NodeInfo ( category = "Action/Renderer/",
                icon = "MeshRenderer",
                description = "Changes the color of the \"Game Object\"")]
    public class ChangeColor : ActionNode {

        /// <summary>
        /// The game object to change the color.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to change the color")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new color.
        /// </summary>
        [VariableInfo(tooltip = "The new color")]
        public ColorVar newColor;

        [System.NonSerialized]
        Renderer m_Renderer = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newColor = Color.white;
        }

        public override Status Update () {
            // Get the renderer
            if (m_Renderer == null || m_Renderer.gameObject != gameObject.Value)
                m_Renderer = gameObject.Value != null ? gameObject.Value.GetComponent<Renderer>() : null;


            // Validate members
            if (m_Renderer == null || newColor.isNone)
                return Status.Error;

            m_Renderer.material.color = newColor.Value;

            return Status.Success;
        }

    }
}