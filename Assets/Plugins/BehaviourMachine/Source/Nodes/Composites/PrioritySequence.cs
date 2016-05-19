//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Similar to the Sequence node, but always starts its execution from the first node.
    /// <seealso cref="BehaviourMachine.Sequence" />
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "PrioritySequence",
                description = "Similar to the Sequence node, but always starts its execution from the first node")]
    public class PrioritySequence : CompositeNode {

        // The last index of the child that was ticked
        [System.NonSerialized]
        int m_LastTickedIndex;

        public override void ResetStatus () {
            base.ResetStatus();
            m_LastTickedIndex = - 1;
        }

        public override Status Update () {
            var childStatus = Status.Error;

            // Tick children
            for (int i = 0; i < children.Length; i++) {
                children[i].OnTick();
                childStatus = children[i].status;

                // The child has not succeeded?
                if (childStatus != Status.Success) {
                    // The last ticked node index is greater than the current ticked node index?
                    if (m_LastTickedIndex > i) {
                        // Reset every node greater than the current node
                        for (int y = i + 1; y < children.Length; y++)
                            children[y].ResetStatus();
                    }
                    // Update last ticked index
                    m_LastTickedIndex = i;
                    // Break loop
                    break;
                }
            }

            return childStatus;
        }
    }
}