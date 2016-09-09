//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Reenables the last state in the target StateMachine. Returns Error if the last enabled state is null.
    /// <seealso cref="BehaviourMachine.StateMachine" />
    /// <seealso cref="BehaviourMachine.InternalBehaviourTree" />
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "StateMachine",
                description = "Reenables the last state in the target StateMachine. Returns Error if the last enabled state is null.")]
    public class ReenabledLastState : ActionNode {

        /// <summary>
        /// The target StateMachine.
        /// </summary>
        [Tooltip("The target StateMachine")]
        public InternalStateMachine stateMachine;

        public override void Reset () {
            stateMachine = null;
        }

        public override Status Update () {
            // Get the last enabled state
            InternalStateBehaviour lastState = stateMachine != null ? stateMachine.lastEnabledState : null;

            // Validate members
            if (lastState == null)
                return Status.Error;

            lastState.enabled = true;
            
            return Status.Success;
        }
    }
}
