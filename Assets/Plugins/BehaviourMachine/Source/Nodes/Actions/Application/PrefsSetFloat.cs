//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of the preference identified by "Key".
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "GameManager",
                description = "Sets the value of the preference identified by \"Key\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.SetFloat.html")]
    public class PrefsSetFloat : ActionNode {

        /// <summary>
        /// Key name.
        /// </summary>
        [VariableInfo (tooltip = "Key name")]
        public StringVar key;

        /// <summary>
        /// The new value.
        /// </summary>
        [VariableInfo(tooltip = "The new value")]
        public FloatVar newValue;

        public override void Reset () {
            key = new ConcreteStringVar();
            newValue = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate Members
            if (key.isNone || newValue.isNone)
                return Status.Error;

            PlayerPrefs.SetFloat(key.Value, newValue.Value);
            return Status.Success;
        }
    }
}