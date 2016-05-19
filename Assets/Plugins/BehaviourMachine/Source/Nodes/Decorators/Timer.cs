//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Runs the child during a specific time.
    /// </summary>
    [NodeInfo(  category = "Decorator/",
                icon = "UnityEditor.AnimationWindow",
                description = "Runs the child during a specific time")]
    public class Timer : DecoratorNode {

        /// <summary>
        /// The interval time in seconds to run the child.
        /// </summary>
        [VariableInfo(tooltip = "The interval time in seconds to run the child")]
        public FloatVar intervalTime;

        /// <summary>
        /// Optionally store the timer.
        /// You can set this value to zero to restart the timer.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Optionally store the timer. You can set this value to zero to restart the timer")]
        public FloatVar storeCurrentTime;

        /// <summary>
        /// Optionally event to send when the time is over.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Send", tooltip = "Optionally event to send when the time is over")]
        public FsmEvent timeOverEvent;

        public override void Reset () {
            intervalTime = 1f;
            storeCurrentTime = new ConcreteFloatVar();
            timeOverEvent = new ConcreteFsmEvent();
        }

        public override Status Update () {
            // Restart?
            if (this.status != Status.Running)
                storeCurrentTime.Value = 0f;

            // Validate members and child
            if (intervalTime.isNone || child == null)
                return Status.Error;

            // Update timer
            storeCurrentTime.Value += owner.deltaTime;

            if (storeCurrentTime.Value >= intervalTime.Value) {
                // Send event?
                if (timeOverEvent.id != 0)
                    owner.root.SendEvent(timeOverEvent.id);

                // The child is running?
                if (child.status == Status.Running)
                    child.ResetStatus();

                // Update status
                return Status.Success;
            }
            else {
                child.OnTick();
                // If the child has Error then returns Error; otherwise returns Running
                return child.status == Status.Error ? Status.Error : Status.Running;
            }
        }
    }
}