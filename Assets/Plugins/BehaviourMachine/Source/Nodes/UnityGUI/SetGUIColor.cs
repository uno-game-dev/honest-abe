//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Use this as child of OnGUI to change the color of your GUI.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "Use this as child of OnGUI to change the color of your GUI.",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI-color.html")]
    public class SetGUIColor : ActionNode, IGUINode {

        /// <summary>
        /// The new color.
        /// </summary>
        [VariableInfo(tooltip = "The new color")]
        public ColorVar newColor;

        public override void Reset () {
            newColor = new ConcreteColorVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || newColor.isNone)
                return Status.Error;

            GUI.color = newColor.Value;
            
            return Status.Success;
        }

        public override void EditorOnTick () {
            OnTick();
        }
    }
}