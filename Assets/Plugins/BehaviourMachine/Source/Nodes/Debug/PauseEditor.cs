//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Pauses the editor.
    /// </summary>
    [NodeInfo(  category = "Debug/",
                icon = "PauseButton",
                description = "Pauses the editor")]
    public class PauseEditor : ActionNode {

        public override Status Update () {
            Debug.Break();
            return Status.Success;
        }
    }
}