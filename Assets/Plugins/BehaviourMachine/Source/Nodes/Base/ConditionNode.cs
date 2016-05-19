//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Base class for condition nodes.
    /// The ConditionNode can has Success or Failure status.
    /// </summary>
    public abstract class ConditionNode : ActionNode {

        /// <summary>
        /// Event to send to the root parent when the condition is met.
        /// </summary>
        [VariableInfo (requiredField = false, nullLabel = "Don't Send", tooltip = "Event to send to the root parent when the condition is met")]
        public FsmEvent onSuccess;

        public override void Reset () {
            onSuccess = new ConcreteFsmEvent();
        }
    }
}