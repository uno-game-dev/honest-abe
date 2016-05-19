//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Writes all modified preferences to disk.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Writes all modified preferences to disk",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.Save.html")]
    public class PrefsSave : ActionNode {

        public override Status Update () {
            PlayerPrefs.Save();
            return Status.Success;
        }
    }
}