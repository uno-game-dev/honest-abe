//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Set this value to false to disable all GUI interaction.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "Set this value to false to disable all GUI interaction",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI-enabled.html")]
    public class SetGUIEnabled : ActionNode, IGUINode {

        /// <summary>
        /// The new value of GUI.enabled.
        /// </summary>
        [VariableInfo(tooltip = "The new value of GUI.enabled")]
        public BoolVar newValue;

        public override void Reset () {
            newValue = new ConcreteBoolVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || newValue.isNone)
                return Status.Error;

            GUI.enabled = newValue.Value;

            return Status.Success;
        }

        public override void EditorOnTick () {
            OnTick();
        }
    }
}