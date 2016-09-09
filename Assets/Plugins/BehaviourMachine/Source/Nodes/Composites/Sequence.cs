//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Think "and" logic. If one child does not succeed, then the execution is stopped and the sequence returns the status of this child.
    /// If a child succeed then sequantially runs the next child.
    /// If all children succeed, returns Success.
    /// <seealso cref="BehaviourMachine.Selector" />
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "Sequence",
                description = "Think \"and\" logic. If all children succeed, returns Success. If one child does not succeed, then the execution is stopped and the sequence returns the status of this child")]
    public class Sequence : CompositeNode {

        [System.NonSerialized]
        int m_CurrentChildIndex = 0;

        public override void Start () {
            if (status == Status.Ready)
                m_CurrentChildIndex = 0;
        }

        public override Status Update () {
            // Returns to the first child when finished
            if (m_CurrentChildIndex >= children.Length) {
                m_CurrentChildIndex = 0;
            }
                
            var childStatus = Status.Error;

            while (m_CurrentChildIndex < children.Length) {
                // Tick current child
                children[m_CurrentChildIndex].OnTick();
                childStatus = children[m_CurrentChildIndex].status;

                // The child succeeded?
                if (childStatus == Status.Success) {
                    // Go to the next child
                    ++m_CurrentChildIndex;
                }
                else {
                    // Break the loop
                    break;
                }
            }

            return childStatus;
        }

        public override void OnValidate () {
            base.OnValidate();
            m_CurrentChildIndex = 0;
        }
    }
}