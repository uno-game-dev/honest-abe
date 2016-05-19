//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Waits an interval of time to tick its child.
    /// </summary>
    [NodeInfo(  category = "Action/Time/",
                icon = "UnityEditor.AnimationWindow",
                description = "Waits an interval of time to tick its child")]
    public class Wait : ActionNode {

        /// <summary>
        /// The interval time in seconds to wait before execute the child again.
        /// </summary>
        [VariableInfo(tooltip = "The interval time in seconds to wait before execute the child again")]
        public FloatVar intervalTime;

        /// <summary>
        /// Optionally store the timer.
        /// You can set this value to zero to restart the timer.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Optionally store the timer. You can set this value to zero to restart the timer.")]
        public FloatVar storeCurrentTime;

        /// <summary>
        /// The status to be returned when finished. 
        /// </summary>
        [Tooltip("The status to be returned when finished")]
        public Status finishedStatus;

        public override void Reset () {
            intervalTime = 1f;
            storeCurrentTime = new ConcreteFloatVar();
            finishedStatus = Status.Success;
        }

        public override void Start () {
            storeCurrentTime.Value = 0f;
        }

        public override Status Update () {
            // Validate members
            if (intervalTime.isNone)
                return Status.Error;

            // Update timer
            storeCurrentTime.Value += owner.deltaTime; 

            // Finished?
            if (storeCurrentTime.Value > intervalTime.Value)
                return finishedStatus;

            return Status.Running;
        }
    }
}