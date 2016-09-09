//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Think "or" logic. If one child does not fails, then the execution is stopped and the selector returns the status of this child. 
    /// If a child fails then sequantially runs the next child.
    /// If all child fails, returns Failure.
    /// <seealso cref="BehaviourMachine.Sequence" />
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "Selector",
                description = "Has an \"or\" logic. If one child succeed, then the execution is stopped and the selector returns Success. If a child fails then sequantially runs the next child. If all child fails, returns Failure")]
    public class Selector : CompositeNode {

        [System.NonSerialized]
        int m_CurrentChildIndex = 0;

        public override void Start () {
            if (status == Status.Ready)
                m_CurrentChildIndex = 0;
        }

        public override Status Update () {
            // Returns to the first child when finished
            if (m_CurrentChildIndex >= children.Length)
                m_CurrentChildIndex = 0;

            var childStatus = Status.Error;
            
            while (m_CurrentChildIndex < children.Length) {
                // Tick current child
                children[m_CurrentChildIndex].OnTick();
                childStatus = children[m_CurrentChildIndex].status;

                // The child failed?
                if (childStatus == Status.Failure) {
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