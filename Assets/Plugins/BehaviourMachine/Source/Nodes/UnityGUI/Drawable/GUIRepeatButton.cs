//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a repeating button. The button returns Success as long as the user holds down the mouse.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make a repeating button. The button returns Success as long as the user holds down the mouse",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI.RepeatButton.html")]
    public class GUIRepeatButton : ConditionNode, IGUINode {

        /// <summary>
        /// Rectangle on the screen to use for the gui control.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Rectangle on the screen to use for the gui control")]
        public RectVar rect;

        /// <summary>
        /// Left coordinate of the rectangle; overrides Rect.x.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Left coordinate of the rectangle; overrides Rect.x")]
        public FloatVar x;

        /// <summary>
        /// Top coordinate of the rectangle; overrides Rect.y.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Top coordinate of the rectangle; overrides Rect.y")]
        public FloatVar y;

        /// <summary>
        /// Width of the rectangle; overrides Rect.width.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Width of the rectangle; overrides Rect.width")]
        public FloatVar width;

        /// <summary>
        /// Height of the rectangle; overrides Rect.height.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Height of the rectangle; overrides Rect.height")]
        public FloatVar height;

        /// <summary>
        /// If True then the Rect values will be normalized.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use OnGUI Defaults", tooltip = "If True then the Rect values will be normalized")]
        public BoolVar normalized;

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

        public Rect GetRect () {
            Rect rect1 = rect.Value;
            
            if (!x.isNone) rect1.x = x;
            if (!y.isNone) rect1.y = y;
            if (!width.isNone) rect1.width = width;
            if (!height.isNone) rect1.height = height;

            if (!normalized.isNone && normalized.Value) {
                // Normalize rect
                rect1.x *= Screen.width;
                rect1.width *= Screen.width;
                rect1.y *= Screen.height;
                rect1.height *= Screen.height;
            }
            else {
                // Scale rect
                rect1.Set(rect1.x / OnGUI.scale, rect1.y / OnGUI.scale, rect1.width / OnGUI.scale, rect1.height / OnGUI.scale);
            }

            return rect1;
        }

        public override void Reset () {
            base.Reset();
            rect = new ConcreteRectVar();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            width = new ConcreteFloatVar();
            height = new ConcreteFloatVar();
            normalized = true;
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
                if (GUI.RepeatButton(this.GetRect(), guiContent))
                    return Status.Success;
                else
                    return Status.Failure;
            }
            else {
                if (GUI.RepeatButton(this.GetRect(), guiContent, guiStyle.Value))
                    return Status.Success;
                else
                    return Status.Failure;
            }
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}