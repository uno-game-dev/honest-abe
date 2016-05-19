//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the time in seconds since the last level has been loaded.
    /// </summary>
    [NodeInfo(  category = "Action/Time/",
                icon = "UnityEditor.AnimationWindow",
                description = "Gets the time in seconds since the last level has been loaded")]
    public class GeTimeSinceLevelLoad : ActionNode {

        /// <summary>
        /// Store the time since the last level has been loaded.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the time since the last level has been loaded")]
        public FloatVar storeTime;

        public override void Reset () {
            storeTime = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (storeTime.isNone)
                return Status.Error;

            storeTime.Value = Time.timeSinceLevelLoad;
            return Status.Success;
        }
    }
}