//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if key exists in the preferences; otherwiser returns Failure.
    /// </summary>
    [NodeInfo ( category = "Condition/Application/",
                icon = "GameManager",
                description = "Returns Success if key exists in the preferences; otherwiser returns Failure",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.DeleteKey.html")]
    public class PrefsHasKey : ConditionNode {

        /// <summary>
        /// The key to test.
        /// </summary>
        [VariableInfo (tooltip = "The key to test")]
        public StringVar key;

        public override void Reset () {
            base.Reset();
            key = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate Members
            if (key.isNone)
                return Status.Error;

            if (PlayerPrefs.HasKey(key.Value)) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}