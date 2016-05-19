//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Similar to the Selector node, but runs all the children at once. You can use this node to run more than one running (highlighted in blue) node at a time.
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "ParallelSelector",
                description = "Similar to the Selector node, but runs all the children at once. You can use this node to run more than one running (highlighted in blue) node at a time")]
    public class ParallelSelector : CompositeNode {

        // The parallel has finished its execution?
        [System.NonSerialized]
        bool m_Fineshed = true;

        public override Status Update () {
            // Restart?
            if (m_Fineshed) {
                // There is at least one child?
                if (children.Length <= 0)
                    return Status.Error;
                else
                    m_Fineshed = false;

                // Tick every child
                for (int i = 0; i < children.Length; i++)
                    children[i].OnTick();
            }
            else {
                // Tick running/success children only
                for (int i = 0; i < children.Length; i++) {
                    if (children[i].status == Status.Running || children[i].status == Status.Success)
                        children[i].OnTick();
                }
            }

            // All children have failed?
            bool childrenFailed = true;
            // Go through all children to check their status
            for (int i = 0; i < children.Length; i++) {
                // Get the child status
                Status childStatus = children[i].status;
                if (childStatus == Status.Running)
                    childrenFailed = false;
                else if (childStatus == Status.Error || childStatus == Status.Success)
                    return childStatus;
            }

            // The execution has been failed?
            if (childrenFailed) {
                m_Fineshed = true;
                return Status.Failure;
            }
            else
                return Status.Running;
        }

        public override void OnValidate () {
            m_Fineshed = true;
        }
    }
}