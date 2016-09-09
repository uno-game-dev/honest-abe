//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a text or texture label on screen.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make a text or texture label on screen",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI.Label.html")]
    public class GUILabel : GUIContentNode {

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            // Get rect
            Rect rect = GetRect();

            // Get gui content
            GUIContent guiContent = GetGUIContent();

            if (guiStyle.isNone)
                GUI.Label(rect, guiContent);
            else
                GUI.Label(rect, guiContent, guiStyle.Value);

            return Status.Success;
        }
    }
}