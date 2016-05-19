//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// A vertical slider the user can drag to change a value between a min (Left Value) and a max (Right Value).
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/Layout/",
                icon = "GUIText",
                description = "A vertical slider the user can drag to change a value between a min (Left Value) and a max (Right Value)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI.VerticalSlider.html")]
    public class VerticalSlider : ActionNode, IGUINode {

        /// <summary>
        /// The value the slider shows. This determines the position of the draggable thumb.
        /// </summary>
        [VariableInfo(requiredField = true, canBeConstant = false, tooltip = "The value the slider shows. This determines the position of the draggable thumb")]
        public FloatVar value;

        /// <summary>
        /// The value at the left end of the slider.
        /// </summary>
        [VariableInfo(tooltip = "The value at the left end of the slider")]
        public FloatVar leftValue;


        /// <summary>
        /// The value at the right end of the slider.
        /// </summary>
        [VariableInfo(tooltip = "The value at the right end of the slider")]
        public FloatVar rightValue;

        public override void Reset () {
            value = new ConcreteFloatVar ();
            leftValue = 0f;
            rightValue = 1f;
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || value.isNone || leftValue.isNone || rightValue.isNone)
                return Status.Error;

            value.Value = GUILayout.VerticalSlider(value.Value, leftValue.Value, rightValue.Value);

            return Status.Success;
        }
    }
}