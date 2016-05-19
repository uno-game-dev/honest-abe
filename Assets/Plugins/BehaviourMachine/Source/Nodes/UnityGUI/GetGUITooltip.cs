//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The tooltip of the control the mouse is currently over, or which has keyboard focus.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "The tooltip of the control the mouse is currently over, or which has keyboard focus",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI-tooltip.html")]
    public class GetGUITooltip : ActionNode, IGUINode {

        /// <summary>
        /// Stores the current tooltip.
        /// </summary>
        [VariableInfo(tooltip = "Stores the current tooltip")]
        public StringVar storeTooltip;

        public override void Reset () {
            storeTooltip = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || storeTooltip.isNone)
                return Status.Error;

            storeTooltip.Value = GUI.tooltip;

            return Status.Success;
        }
    }
}