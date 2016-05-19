//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Removes all keys and values from the preferences. Use with caution.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Removes all keys and values from the preferences. Use with caution",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.DeleteAll.html")]
    public class PrefsDeleteAll : ActionNode {

        public override Status Update () {
            PlayerPrefs.DeleteAll();
            return Status.Success;
        }
    }
}