//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Runs all the children at once. You can use this node to run more than one running (highlighted in blue) node at a time. The execution is stopped if a child returns Error. Returns the status of the last node.
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "Parallel",
                description = "Runs all the children at once. You can use this node to run more than one running (highlighted in blue) node at a time. The execution is stopped if a child returns Error. Returns the status of the last node")]
    public class Parallel : CompositeNode {

        public override Status Update () {
            Status childStatus = Status.Error;

            // Tick children
            for (int i = 0; i < children.Length; i++) {
                // Tick children
                children[i].OnTick();
                // Get the child status
                childStatus = children[i].status;
                // The child returned Error?
                if (childStatus == Status.Error)
                    break;
            }

            return childStatus;
        }
    }
}