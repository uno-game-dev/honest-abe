//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sends an event to all enabled root FSM in the scene.
    /// Please note that this node is very slow. It is not recommended to use this node every frame.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "StateMachine",
                description = "Sends an event to all enabled root FSM in the scene. Please note that this node is very slow. It is not recommended to use this node every frame")]
    public class SendEventToAll : ActionNode {

        /// <summary>
        /// The event to send.
        /// </summary>
        [Tooltip("The event to send")]
        public FsmEvent eventToSend;

        /// <summary>
        /// Store true if the event was used; false otherwise.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store true if the event was used; false otherwise")]
        public BoolVar storeEventUsed;

        public override void Reset () {
            eventToSend = new ConcreteFsmEvent();
            storeEventUsed = new ConcreteBoolVar();
        }

    	public override Status Update () {
            // Validate members
            if (eventToSend.isNone)
                return Status.Error;

            storeEventUsed.Value = InternalBlackboard.SendEventToAll(eventToSend.id);

            return Status.Success;
        }
    }
}
