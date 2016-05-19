//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Base class for gui nodes that need a rect.
    /// </summary>
    public abstract class GUIRectNode : ActionNode, IGUINode {

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

    	public override void Reset () {
            rect = new ConcreteRectVar();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            width = new ConcreteFloatVar();
            height = new ConcreteFloatVar();
            normalized = true;
        }

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
    }
}