//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Enable and tick a state.
    /// </summary>
    [NodeInfo(  category = "Action/State/",
                icon = "StateMachine",
                description = "Enable and tick a state")]
    public class StateNode : ActionNode {

        /// <summary>
        /// The state to be enabled and ticked.
        /// </summary>
        [TreeState]
        [Tooltip("The state to be enabled and ticked")]
        public InternalStateBehaviour state;

        public override void Reset () {
            state = null;
        }

        public override void OnValidate () {
            if (state != null && state.parent != tree)
                state = null;
        }

        public override void Start () {
            if (state != null && !state.enabled)
                state.enabled = true;
        }

        public override Status Update () {
            // Validate members
            if (state == null)
                return Status.Error;
            

            return state.OnTick();
        }

        public override void End () {
            if (state != null && state.enabled)
                state.enabled = false;
        }
    }
}
