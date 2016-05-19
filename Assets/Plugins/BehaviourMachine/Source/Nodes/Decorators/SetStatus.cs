//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the child returned status to "New Status".
    /// </summary>
    [NodeInfo(  category = "Decorator/",
                icon = "Status",
                description = "Changes the child returned status to \"New Status\"")]
    public class SetStatus : DecoratorNode {

        /// <summary>
        /// The new status.
        /// </summary>
        [Tooltip("The new status")]
        public Status newStatus;

        public override void Reset () {
            newStatus = Status.Success;
        }

        public override Status Update () {
            if (child != null)
                child.OnTick();
            return newStatus;
        }
    }
}
