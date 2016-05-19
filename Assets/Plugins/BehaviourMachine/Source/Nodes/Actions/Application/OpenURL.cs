//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Opens the url in a browser.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Opens the url in a browser",
                url = "http://docs.unity3d.com/ScriptReference/Application.OpenURL.html")]
    public class OpenURL : ActionNode {

        /// <summary>
        /// The url to open.
        /// </summary>
        [VariableInfo (tooltip = "The url to open")]
        public StringVar url;

        public override void Reset () {
            url = "www.behaviourmachine.com";
        }

        public override Status Update () {
            // Validate Members
            if (url.isNone)
                return Status.Error;

            Application.OpenURL(url.Value);
            return Status.Success;
        }
    }
}