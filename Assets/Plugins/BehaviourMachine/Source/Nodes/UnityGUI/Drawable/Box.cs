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
    [NodeInfo(  category = "UnityGUI/Drawable/Layout/",
                icon = "GUIText",
                description = "Make an auto-layout box",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.Box.html")]
    public class Box : GUILayoutContentNode {

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            if (guiStyle.isNone)
                GUILayout.Box(GetGUIContent(), LayoutOptions.GetCurrent());
            else
                GUILayout.Box(GetGUIContent(), guiStyle.Value, LayoutOptions.GetCurrent());

            return Status.Success;
        }
    }
}