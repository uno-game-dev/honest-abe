//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a single press button. The user clicks them and something happens immediately.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/Layout/",
                icon = "GUIText",
                description = "Make a single press button. The user clicks them and something happens immediately",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.Button.html")]
    public class Button : ConditionNode, IGUINode {

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
            base.Reset();
            texture = new ConcreteTextureVar();
            text = new ConcreteStringVar();
            tooltip = new ConcreteStringVar();
            guiStyle = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            // Get GUIContent
            GUIContent guiContent = new GUIContent(text.Value, texture.Value, tooltip.Value);

            if (guiStyle.isNone) {
                if (GUILayout.Button(guiContent, LayoutOptions.GetCurrent())) {
                    // Send event?
                    if (onSuccess.id != 0)
                        owner.root.SendEvent(onSuccess.id);

                    return Status.Success;
                }
                else
                    return Status.Failure;
            }
            else {
                if (GUILayout.Button(guiContent, guiStyle.Value, LayoutOptions.GetCurrent())) {
                    // Send event?
                    if (onSuccess.id != 0)
                        owner.root.SendEvent(onSuccess.id);
                        
                    return Status.Success;
                }
                else
                    return Status.Failure;
            }
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}