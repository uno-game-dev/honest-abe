//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sends an event to the "Other" game object, always target the root enabled FSMs.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "StateMachine",
                description = "Sends an event to the \"Other\" game object, always target the root enabled FSMs")]
    public class SendEventToOther : ActionNode {

        /// <summary>
        /// The game object to send an event. BroadCast Event sends the event to all game objects that have a blackboard component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "BroadCast Event", tooltip = "The game object to send an event. BroadCast Event sends the event to all game objects that has a blackboard component.")]
        public GameObjectVar other;

        /// <summary>
        /// The event to send.
        /// </summary>
        [Tooltip("The event to send")]
        public FsmEvent eventToSend;

        /// <summary>
        /// Optionally specify the name of the target FSM. If none then the event will be sent to all root enabled FSMs in the "Game Object"?.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "All Root FSMs", tooltip = "Optionally specify the name of the target FSM. If none then the event will be sended to all root enabled FSMs in the \"Game Object\"?")]
        public StringVar stateName;

        /// <summary>
        /// Store true if the event was used; false otherwise.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store true if the event was used; false otherwise")]
        public BoolVar storeEventUsed;

        public override void Reset () {
            other = new ConcreteGameObjectVar();
            eventToSend = new ConcreteFsmEvent();
            stateName = new ConcreteStringVar();
            storeEventUsed = new ConcreteBoolVar();
        }

    	public override Status Update () {
            // Validate members
            if (eventToSend.isNone)
                return Status.Error;

            // Broadcast event?
            if (other.isNone) {
                foreach (BehaviourMachine.InternalBlackboard blackboard in Object.FindObjectsOfType(typeof(BehaviourMachine.InternalBlackboard))) {
                    // The state name is none?
                    if (stateName.isNone)
                        storeEventUsed.Value = blackboard.SendEvent(eventToSend.id);
                    else
                        storeEventUsed.Value = blackboard.SendEvent(stateName.Value, eventToSend.id);
                }
                storeEventUsed.Value = true;
            }
            else {
                // Get the game object's blackboard
                var blackboard = other.Value != null ? other.Value.GetComponent<BehaviourMachine.InternalBlackboard>() : null;

                // Validate blackboard
                if (blackboard == null)
                    return Status.Error;

                // The state name is none?
                if (stateName.isNone)
                    storeEventUsed.Value = blackboard.SendEvent(eventToSend.id);
                else
                    storeEventUsed.Value = blackboard.SendEvent(stateName.Value, eventToSend.id);
            }

            return Status.Success;
        }
    }
}
