//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make an auto-layout box.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make an auto-layout box",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI.Box.html")]
    public class GUIBox : GUIContentNode {

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            if (guiStyle.isNone)
                GUI.Box(this.GetRect(), GetGUIContent());
            else
                GUI.Box(this.GetRect(), GetGUIContent(), guiStyle.Value);

            return Status.Success;
        }
    }
}