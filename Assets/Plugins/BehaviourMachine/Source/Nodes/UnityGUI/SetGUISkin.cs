//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Use this as child of OnGUI to change the look of your GUI.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "Use this as child of OnGUI to change the look of your GUI.",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI-skin.html")]
    public class SetGUISkin : ActionNode, IGUINode {

        /// <summary>
        /// The new GUISkin.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Default", tooltip = "The new GUISkin")]
        public ObjectVar newGuiskin;

        public override void Reset () {
            newGuiskin = new ConcreteObjectVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            GUI.skin = newGuiskin.isNone ? null : newGuiskin.Value as GUISkin;

            return Status.Success;
        }

        public override void EditorOnTick () {
            OnTick();
        }
    }
}