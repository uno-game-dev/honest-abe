//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Workaround to override the OnEnable and OnDisable methods on the nodes OnEnable and OnDisable.
    /// <seealso cref="BehaviourMachine.OnEnable" />
    /// <seealso cref="BehaviourMachine.OnDisable" />
    /// </summary>
    public abstract class OnEnableDisableOwner : FunctionNode {

        public override void OnEnable() {
            this.OnEnableOwner();
        }

        public override void OnDisable() {
            this.OnDisableOwner();
        }

        public abstract void OnEnableOwner ();

        public abstract void OnDisableOwner ();
    }
}