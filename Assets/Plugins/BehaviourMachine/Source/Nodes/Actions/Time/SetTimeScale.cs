//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a new value in the scale at which the time is passing.
    /// </summary>
    [NodeInfo(  category = "Action/Time/",
                icon = "UnityEditor.AnimationWindow",
                description = "Set's a new value in the scale at which the time is passing",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Time-timeScale.html")]
    public class SetTimeScale : ActionNode {

        /// <summary>
        /// The new value of Time.timeScale.
        /// </summary>
        [VariableInfo(tooltip = "The new value of Time.timeScale")]
        public FloatVar newTimeScale;

        public override void Reset () {
            newTimeScale = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (newTimeScale.isNone)
                return Status.Error;

            Time.timeScale = newTimeScale.Value;

            return Status.Success;
        }
    }
}