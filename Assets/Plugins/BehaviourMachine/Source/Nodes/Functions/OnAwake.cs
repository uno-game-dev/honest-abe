//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Workaround to override the Awake methods on the nodes Awake node.
    /// <seealso cref="BehaviourMachine.Awake" />
    /// </summary>
    public abstract class OnAwake : FunctionNode {

        public override void Awake () {
            this.OnAwakeTree();
        }

        public abstract void OnAwakeTree ();
    }
}