//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sends an event, always send the event to the root state.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "StateMachine",
                description = "Sends an event, always send the event to the root state")]
    public class SendEvent : ActionNode {

        /// <summary>
        /// The event to send.
        /// </summary>
        [VariableInfo(tooltip = "The event to send")]
        public FsmEvent eventToSend;

        /// <summary>
        /// Optional delay in seconds.
        /// </summary>
        [Range(0f, 10f)]
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optional delay in seconds.")]
        public FloatVar delay;

        /// <summary>
        /// Optionally store the delay timer.
        /// You can set this value to zero to restart the delay timer.
        /// Note: This value is set to zero when the tree is enabled.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Optionally store the delay timer. You can set this value to zero to restart the delay timer. Note: This value is set to zero when the tree is enabled")]
        public FloatVar storeDelayTime;

        /// <summary>
        /// Store true if the event was used; false otherwise.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store true if the event was used; false otherwise")]
        public BoolVar storeEventUsed;


        public override void Reset () {
            eventToSend = new ConcreteFsmEvent();
            delay = new ConcreteFloatVar();
            storeEventUsed = new ConcreteBoolVar();
            storeDelayTime = new ConcreteFloatVar();
        }

        public override void Start () {
            storeDelayTime.Value = delay.Value;
        }

        public override Status Update () {
            // Validate members
            if (eventToSend.isNone)
                return Status.Error;

            if (storeDelayTime.Value <= 0f) {
                storeEventUsed.Value = owner.SendEvent(eventToSend.id);
                return Status.Success;
            }
            else {
                storeDelayTime.Value -= owner.deltaTime;
                return Status.Running;
            }
        }

        public override void End () {
            storeDelayTime.Value = delay.Value;
        }
    }
}
