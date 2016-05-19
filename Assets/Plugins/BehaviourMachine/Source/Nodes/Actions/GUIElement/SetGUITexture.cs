//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the texture of a GUITexture.
    /// </summary>
    [NodeInfo(  category = "Action/GUIElement/",
                icon = "GUITexture",
                description = "Changes the texture of a GUITexture",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/SetGUITexture.html")]
    public class SetGUITexture : ActionNode {

        /// <summary>
        /// The game object that has a gui texture in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a gui texture in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new texture to display.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new texture to display")]
        public TextureVar newTexture;

        /// <summary>
        /// The new color of the texture.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new color of the texture")]
        public ColorVar newColor;

        [System.NonSerialized]
        GUITexture m_SetGUITexture;

        public override void Reset () {
            newTexture = new ConcreteTextureVar();
            newColor = new ConcreteColorVar();
        }

        public override Status Update () {
            // Get the gui texture
            if (m_SetGUITexture == null || m_SetGUITexture.gameObject != gameObject.Value)
                m_SetGUITexture = gameObject.Value != null ? gameObject.Value.GetComponent<GUITexture>() : null;

            // Validate members
            if (m_SetGUITexture == null)
                return Status.Error;

            if (!newTexture.isNone) m_SetGUITexture.texture = newTexture.Value;
            if (!newColor.isNone) m_SetGUITexture.color = newColor.Value;
            
            return Status.Success;
        }
    }
}