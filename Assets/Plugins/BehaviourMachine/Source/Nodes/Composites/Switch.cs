//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Ticks the child in the supplied "Index". Ticks only one child at a time. The first child index is zero. Returns Error if the "Index" is negative or is above the children's maximum index.
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "Switch",
                description = "Ticks the child in the supplied \"Index\". Ticks only one child at a time. The first child index is zero. Returns Error if the \"Index\" is negative or is above the children\'s maximum index")]
    public class Switch : CompositeNode {

        /// <summary>
        /// The index of the child to tick. The first child is on the index 0.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The index of the child to tick. The first child is on the index 0")]
        public IntVar index;

        [System.NonSerialized]
        int m_LastChildIndex = -1;

        private void ResetLastChild () {
            // The last child is valid?
            if (m_LastChildIndex >= 0 && m_LastChildIndex < children.Length && children[m_LastChildIndex].status == Status.Running) {
                // Call ResetStatus() on the last child
                children[m_LastChildIndex].ResetStatus();
            }
        }

        public override void Reset () {
            index = 0;
        }

        public override Status Update () {
            // Get the current index
            var currentIndex = index.Value;
            
            // Validate members
            if (index.isNone || currentIndex < 0 || currentIndex >= children.Length) {
                ResetLastChild();
                return Status.Error;
            }

            // Restart?
            if (this.status == Status.Ready)
                m_LastChildIndex = currentIndex;

            // The index changed?
            if (m_LastChildIndex != currentIndex)
                ResetLastChild();

            // Tick the current child
            children[currentIndex].OnTick();
            // Store current index
            m_LastChildIndex = currentIndex;

            return children[currentIndex].status;
        }
    }
}