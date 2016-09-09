//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the value of a int corresponding to key in the preference file if it exists.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Gets the value of a int corresponding to key in the preference file if it exists",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.GetInt.html")]
    public class PrefsGetInt : ActionNode {

        /// <summary>
        /// Key name.
        /// </summary>
        [VariableInfo (tooltip = "Key name")]
        public StringVar key;

        /// <summary>
        /// Store the value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the value")]
        public IntVar store;

        public override void Reset () {
            key = new ConcreteStringVar();
            store = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate Members
            if (key.isNone || store.isNone)
                return Status.Error;

            store.Value = PlayerPrefs.GetInt(key.Value);
            return Status.Success;
        }
    }
}