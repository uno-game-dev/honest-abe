//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Do a GUILayout block of GUI controls in a fixed screen area.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Group/",
                icon = "GUILayer",
                description = "Do a GUILayout block of GUI controls in a fixed screen area",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.BeginArea.html")]
    public class LayoutArea : CompositeNode, IGUINode {

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
            rect = new ConcreteRectVar();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            width = new ConcreteFloatVar();
            height = new ConcreteFloatVar();

            texture = new ConcreteTextureVar();
            text = new ConcreteStringVar();
            tooltip = new ConcreteStringVar();
            guiStyle = new ConcreteStringVar();
        }


    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            // Get the screen rect
            Rect screenRect = rect.Value;
            if (!x.isNone) screenRect.x = x;
            if (!y.isNone) screenRect.y = y;
            if (!width.isNone) screenRect.width = width;
            if (!height.isNone) screenRect.height = height;

            // Scale the screen rect
            screenRect.Set(screenRect.x / OnGUI.scale , screenRect.y / OnGUI.scale , screenRect.width / OnGUI.scale , screenRect.height / OnGUI.scale);

            // Get the gui content
            GUIContent guiContent = new GUIContent(text.Value, texture.Value, tooltip.Value);

            if (guiStyle.isNone)
                GUILayout.BeginArea(screenRect, guiContent);
            else
                GUILayout.BeginArea(screenRect, guiContent, guiStyle.Value);

            Status currentStatus = base.Update();

            GUILayout.EndArea();

            return currentStatus;
        }

        public override void  EditorOnTick () {
            // Is OnGUI?
            if (Event.current == null)
                return;

            // Get the screen rect
            Rect screenRect = rect.Value;
            if (!x.isNone) screenRect.x = x;
            if (!y.isNone) screenRect.y = y;
            if (!width.isNone) screenRect.width = width;
            if (!height.isNone) screenRect.height = height;

            // Scale the screen rect
            screenRect.Set(screenRect.x / OnGUI.scale , screenRect.y / OnGUI.scale , screenRect.width / OnGUI.scale , screenRect.height / OnGUI.scale);

            // Get the gui content
            GUIContent guiContent = new GUIContent(text.Value, texture.Value, tooltip.Value);

            if (guiStyle.isNone)
                GUILayout.BeginArea(screenRect, guiContent);
            else
                GUILayout.BeginArea(screenRect, guiContent, guiStyle.Value);

            base.EditorOnTick();

            GUILayout.EndArea();
        }
    }
}