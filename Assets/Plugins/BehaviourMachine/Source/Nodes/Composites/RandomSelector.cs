//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    /// <summary>
    /// Same as Selector but always shuffles the children before tick.
    /// <seealso cref="BehaviourMachine.Selector" />    
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "RandomSelector",
                description = "Same as Selector but always shuffles the children before tick")]
    public class RandomSelector : RandomChild {

        // The order to run the children
        [System.NonSerialized]
        List<int> m_ChildrenIndex = new List<int>();

        public int randomChildIndexInList {
            get {
                // Create the list of the current children weights
                var realativeWeights = new List<float>();
                for (int i = 0; i < m_ChildrenIndex.Count; i++)
                    realativeWeights.Add(weight[m_ChildrenIndex[i]]);

                // Get a random index on the list
                int randomIndex = NodeUtility.GetRandomIndexFor(realativeWeights.ToArray());

                // It is a valid index?
                if (randomIndex >= 0) {
                    // Get the child index on the children array
                    int currentChildIndex = m_ChildrenIndex[randomIndex];
                    // Removes the selected index
                    m_ChildrenIndex.RemoveAt(randomIndex);

                    return currentChildIndex;
                }

                return -1;
            }
        }

        public override Status Update () {
            // Validate children
            if (children.Length <= 0)
                return Status.Error;

            // Restart?
            if (this.status == Status.Ready || m_CurrentChildIndex < 0) {
                // Clear the children index list
                m_ChildrenIndex.Clear();

                // Populate the list with its indexes
                for (int i = 0; i < children.Length; i++)
                    m_ChildrenIndex.Add(i);

                // Get the first child to tick
                m_CurrentChildIndex = this.randomChildIndexInList;
            }

            var childStatus = Status.Error;
            
            while (true) {
                // Tick current child
                children[m_CurrentChildIndex].OnTick();
                childStatus = children[m_CurrentChildIndex].status;

                // The child succeded?
                if (childStatus == Status.Failure) {
                    m_CurrentChildIndex = this.randomChildIndexInList;
                    // Is there a next child to tick?
                    if (m_CurrentChildIndex < 0) {
                        // Finish exection
                        break;
                    }
                }
                else {
                    // Break the loop
                    break;
                }
            }

            return childStatus;
        }
    }
}