//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the color of a GUIText.
    /// </summary>
    [NodeInfo(  category = "Action/GUIElement/",
                icon = "GUIText",
                description = "Changes the color of a GUIText",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUIText.html")]
    public class SetGUITextColor : ActionNode {

        /// <summary>
        /// The game object that has a gui text in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a gui text in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new text color.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change",tooltip = "The new text color")]
        public ColorVar newColor;

        [System.NonSerialized]
        GUIText m_GUIText;

        public override void Reset () {
            newColor = new ConcreteColorVar();
        }

        public override Status Update () {
            // Get the gui text
            if (m_GUIText == null || m_GUIText.gameObject != gameObject.Value)
                m_GUIText = gameObject.Value != null ? gameObject.Value.GetComponent<GUIText>() : null;

            // Validate members
            if (m_GUIText == null)
                return Status.Error;

            // Set Color
            #if !UNITY_4_0_0 && !UNITY_4_1
            if (!newColor.isNone) m_GUIText.color = newColor.Value;
            #else
            if (!newColor.isNone) m_GUIText.material.color = newColor.Value;
            #endif

            return Status.Success;
        }
    }
}