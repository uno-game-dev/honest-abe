//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Removes "Key" and its corresponding value from the preferences.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Removes \"Key\" and its corresponding value from the preferences",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.DeleteKey.html")]
    public class PrefsDeleteKey : ActionNode {

        /// <summary>
        /// The key to delete.
        /// </summary>
        [VariableInfo (tooltip = "The key to delete")]
        public StringVar key;

        public override void Reset () {
            key = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate Members
            if (key.isNone)
                return Status.Error;

            PlayerPrefs.DeleteKey(key.Value);
            return Status.Success;
        }
    }
}