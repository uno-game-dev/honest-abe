//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Quits the player application.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "SceneAsset",
                description = "Quits the player application",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Application.Quit.html")]
    public class ApplicationQuit : ActionNode {

        public override Status Update () {
            Application.Quit();
            return Status.Success;
        }
    }
}