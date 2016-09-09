//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make an on/off toggle button.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/Layout/",
                icon = "GUIText",
                description = "Make an on/off toggle button",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.Toggle.html")]
    public class Toggle : GUILayoutContentNode {

        /// <summary>
        /// Is the button on or off?
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Is the button on or off?")]
        public BoolVar Value;

        public override void Reset () {
            base.Reset();
            Value = new ConcreteBoolVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || Value.isNone)
                return Status.Error;

            if (guiStyle.isNone)
                Value.Value = GUILayout.Toggle(Value.Value, GetGUIContent(), LayoutOptions.GetCurrent());
            else
                Value.Value = GUILayout.Toggle(Value.Value, GetGUIContent(), guiStyle.Value, LayoutOptions.GetCurrent());

            return Status.Success;
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}