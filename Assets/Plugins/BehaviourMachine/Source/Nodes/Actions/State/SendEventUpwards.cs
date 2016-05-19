//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sends an event to all enabled root FSM in the "Game Object" and its ancestor.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "StateMachine",
                description = "Sends an event to all enabled root FSM in the \"Game Object\" and its ancestor")]
    public class SendEventUpwards : ActionNode {

        /// <summary>
        /// The target game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The target game object")]
        public GameObjectVar gameObject;

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
            gameObject = new ConcreteGameObjectVar(this.self);
            eventToSend = new ConcreteFsmEvent();
            storeEventUsed = new ConcreteBoolVar();
        }

    	public override Status Update () {
            // Get the target blackboard
            var blackboard = gameObject.Value == null ? self.GetComponent<InternalBlackboard>() : gameObject.Value.GetComponent<InternalBlackboard>();

            // Validate members
            if (blackboard == null || eventToSend.isNone)
                return Status.Error;

            storeEventUsed.Value = blackboard.SendEventUpwards(eventToSend.id);

            return Status.Success;
        }
    }
}
