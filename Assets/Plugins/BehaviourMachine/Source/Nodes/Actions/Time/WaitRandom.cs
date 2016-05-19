//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Waits a random interval of time.
    /// </summary>
    [NodeInfo(  category = "Action/Time/",
                icon = "UnityEditor.AnimationWindow",
                description = "Waits a random interval of time")]
    public class WaitRandom : ActionNode {

        /// <summary>
        /// The minimum interval time in seconds to wait.
        /// </summary>
        [VariableInfo(tooltip = "The interval time in seconds to wait")]
        public FloatVar min;

        /// <summary>
        /// The maximum interval time in seconds to wait.
        /// </summary>
        [VariableInfo(tooltip = "The interval time in seconds to wait")]
        public FloatVar max;

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

        [System.NonSerialized]
        float m_Time;

        public override void Reset () {
            min = .5f;
            max = 1.5f;
            storeCurrentTime = new ConcreteFloatVar();
            finishedStatus = Status.Success;
        }

        public override void Start () {
            storeCurrentTime.Value = 0f;
            m_Time = Random.Range(min.Value, max.Value);
        }

        public override Status Update () {
            // Validate members
            if (min.isNone || max.isNone)
                return Status.Error;

            // Update timer
            storeCurrentTime.Value += owner.deltaTime; 

            // Finished?
            if (storeCurrentTime.Value > m_Time)
                return finishedStatus;

            return Status.Running;
        }
    }
}