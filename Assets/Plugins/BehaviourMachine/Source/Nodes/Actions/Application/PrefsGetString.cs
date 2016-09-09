//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the value of a string corresponding to key in the preference file if it exists.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Gets the value of a string corresponding to key in the preference file if it exists",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.GetString.html")]
    public class PrefsGetString : ActionNode {

        /// <summary>
        /// Key name.
        /// </summary>
        [VariableInfo (tooltip = "Key name")]
        public StringVar key;

        /// <summary>
        /// Store the value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the value")]
        public StringVar store;

        public override void Reset () {
            key = new ConcreteStringVar();
            store = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (key.isNone || store.isNone)
                return Status.Error;

            store.Value = PlayerPrefs.GetString(key.Value);
            return Status.Success;
        }
    }
}