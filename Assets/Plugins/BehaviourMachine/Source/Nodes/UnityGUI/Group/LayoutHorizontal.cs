//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Begin a Horizontal control group.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Group/",
                icon = "GUILayer",
                description = "Begin a Horizontal control group",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.BeginHorizontal.html")]
    public class LayoutHorizontal : CompositeNode, IGUINode {

        /// <summary>
        /// The icon image contained.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The icon image contained")]
        public TextureVar texture;

        /// <summary>
        /// The text contained.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The text contained")]
        public StringVar text;

        /// <summary>
        /// The tooltip of this element.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The tooltip of this element")]
        public StringVar tooltip;

        /// <summary>
        /// The style to use.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "The style to use")]
        public StringVar guiStyle;

        public override void Reset () {
            texture = new ConcreteTextureVar();
            text = new ConcreteStringVar();
            tooltip = new ConcreteStringVar();
            guiStyle = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            // Get the gui content
            GUIContent guiContent = new GUIContent(text.Value, texture.Value, tooltip.Value);

            if (guiStyle.isNone)
                GUILayout.BeginHorizontal(guiContent, GUIStyle.none, LayoutOptions.GetCurrent());
            else
                GUILayout.BeginHorizontal(guiContent, guiStyle.Value, LayoutOptions.GetCurrent());

            Status currentStatus = base.Update();

            GUILayout.EndHorizontal();

            return currentStatus;
        }

        public override void EditorOnTick () {
            // Is OnGUI?
            if (Event.current == null)
                return;

            // Get the gui content
            GUIContent guiContent = new GUIContent(text.Value, texture.Value, tooltip.Value);

            if (guiStyle.isNone)
                GUILayout.BeginHorizontal(guiContent, GUIStyle.none, LayoutOptions.GetCurrent());
            else
                GUILayout.BeginHorizontal(guiContent, guiStyle.Value, LayoutOptions.GetCurrent());

            base.EditorOnTick();

            GUILayout.EndHorizontal();
        }
    }
}