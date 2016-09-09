//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Insert a space in the current layout group.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "Insert a space in the current layout group",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.Space.html")]
    public class LayoutSpace : ActionNode, IGUINode {

        /// <summary>
        /// The size of the space in pixels.
        /// </summary>
        [VariableInfo(tooltip = "The size of the space in pixels")]
        public FloatVar pixels;

        public override void Reset () {
            pixels = new ConcreteFloatVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || pixels.isNone)
                return Status.Error;

            GUILayout.Space(pixels.Value / OnGUI.scale);

            return Status.Success;
        }

        public override void EditorOnTick () {
            OnTick ();
        }
    }
}