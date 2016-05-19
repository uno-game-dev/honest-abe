//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Flexible spaces use up any leftover space in a layout.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "Flexible spaces use up any leftover space in a layout",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.Space.html")]
    public class LayoutFlexibleSpace : ActionNode, IGUINode {

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            GUILayout.FlexibleSpace();

            return Status.Success;
        }

        public override void EditorOnTick () {
            OnTick ();
        }
    }
}