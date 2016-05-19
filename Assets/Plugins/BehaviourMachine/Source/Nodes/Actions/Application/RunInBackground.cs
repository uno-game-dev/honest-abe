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
                description = "Should the player be running when the application is in the background?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Application-runInBackground.html")]
    public class RunInBackground : ActionNode {

        [VariableInfo(tooltip = "Default is False (application pauses when it is in background)")]
        public BoolVar runInBackground;

        public override void Reset () {
            runInBackground = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate Members
            if (runInBackground.isNone)
                return Status.Error;

            Application.runInBackground = runInBackground.Value;
            return Status.Success;
        }
    }
}