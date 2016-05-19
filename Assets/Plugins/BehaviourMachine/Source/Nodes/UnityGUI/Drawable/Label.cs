//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make an auto-layout label.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/Layout/",
                icon = "GUIText",
                description = "Make an auto-layout label",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.Label.html")]
    public class Label : GUILayoutContentNode {

        /// <summary>
        /// Optional Float to display.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optional Float to display")]
        public FloatVar floatVar;

        /// <summary>
        /// Optional Int to display.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optional Int to display")]
        public IntVar intVar;

        public override void Reset () {
            base.Reset();
            floatVar = new ConcreteFloatVar();
            intVar = new ConcreteIntVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            // Get gui content
            GUIContent guiContent = GetGUIContent();

            // Add optional vars to the gui content
            if (!floatVar.isNone) guiContent.text += floatVar.Value;
            if (!intVar.isNone) guiContent.text += intVar.Value;

            if (guiStyle.isNone)
                GUILayout.Label(guiContent, LayoutOptions.GetCurrent());
            else
                GUILayout.Label(guiContent, guiStyle.Value, LayoutOptions.GetCurrent());

            return Status.Success;
        }
    }
}