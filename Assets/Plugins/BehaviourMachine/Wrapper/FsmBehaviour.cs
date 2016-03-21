//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Wrapper class for the InternalStateMachine component.
    /// <summary>
    [AddComponentMenu("")]
    [System.Obsolete ("Please use the StateMachine instead. Drag and drop the Plugins/BehaviourMachine/Wrapper/StateMachine.cs script in the Script property of the FsmBehaviour.")]
    public class FsmBehaviour : StateMachine {

        /// <summary>
        /// Prints the obsolete message.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();
            Print.LogWarning("FsmBehaviour is obsolete, please use the StateMachine component. Drag and drop the Plugins/BehaviourMachine/Wrapper/StateMachine.cs script in the Script property of the FsmBehaviour.");
        }
    }
}