//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    /// <summary>
    /// Randomly choose a child to tick.
    /// You can set the relative weight of each child to control how often they are selected.
    /// <seealso cref="BehaviourMachine.SetRandomSeed" />
    /// <summary>
    [NodeInfo ( category = "Composite/",
                icon = "RandomChild",
                description = "Randomly choose a child to tick. You can set the relative weight of each child to control how often they are selected")]
    public class RandomChild : CompositeNode {

        /// <summary>
        /// The relative weight of the child.
        /// </summary>
        [Tooltip("The relative weight of the children")]
        [HideInInspector]
        [Range(0f, 1f)]
        public FloatVar[] weight;

        /// <summary>
        /// Stores the children index.
        /// </summary>
        [HideInInspector]
        public int[] childrenID;

        /// <summary>
        /// The current child.
        /// </summary>
        [System.NonSerialized]
        protected int m_CurrentChildIndex = -1;

        /// <summary>
        /// Call this to get a random child index.
        /// </summary>
        public int randomChildIndex {
            get {
                if (this.children.Length != weight.Length || this.children.Length != childrenID.Length)
                    this.OnValidate();
                return NodeUtility.GetRandomIndexFor(weight);
            }
        }

        public override void Reset () {
            weight = new FloatVar[0];
            childrenID = new int[0];
            this.OnValidate();
        }

        
        public override Status Update () {
            // Validate children
            if (this.children.Length <= 0)
                return Status.Error;

            // Select a new child?
            if (this.status == Status.Ready || m_CurrentChildIndex < 0)
                m_CurrentChildIndex = this.randomChildIndex;

            var childStatus = Status.Error;
            
            // Tick child
            if (m_CurrentChildIndex >= 0 && m_CurrentChildIndex < children.Length) {
                this.children[m_CurrentChildIndex].OnTick();
                childStatus = this.children[m_CurrentChildIndex].status;

                if (childStatus == Status.Success)
                    m_CurrentChildIndex = -1;
            }

            return childStatus;
        }

        public override void OnValidate () {
            // Reset execution
            m_CurrentChildIndex = -1;

            // Create a list of weights
            var newWeight = new List<FloatVar>(weight);
            // Create a list of children index
            var newChildrenID = new List<int>(childrenID);

            // Reset data if the lists have different sizes
            if (newWeight.Count != newChildrenID.Count) {
                newWeight.Clear();
                newChildrenID.Clear();
            }

            // Remove the lefover nodes
            if (newChildrenID.Count > children.Length) {
                // Get the current list of ids
                var currentChildrenID = new List<int>();
                for (int i = 0; i < children.Length; i++)
                    currentChildrenID.Add(children[i].instanceID);

                // Check if a child was removed
                for (int i = newChildrenID.Count - 1; i >= 0; i--) {
                    if (!currentChildrenID.Contains(newChildrenID[i])) {
                        newChildrenID.RemoveAt(i);
                        newWeight.RemoveAt(i);
                    }
                }
            }

            // A child node was add/moved?
            for (int i = 0; i < children.Length; i++) {
                // New child?
                if (i >= newChildrenID.Count) {
                    newChildrenID.Add(children[i].instanceID);
                    newWeight.Add(new ConcreteFloatVar(.5f));
                }
                // The child was moved?
                else if (children[i].instanceID != newChildrenID[i]) {
                    int currentIndex = newChildrenID.IndexOf(children[i].instanceID);
                    if (currentIndex != -1) {
                        var variable = newWeight[i];
                        newWeight[i] = newWeight[currentIndex];
                        newWeight[currentIndex] = variable;
                    }

                    if (currentIndex >= 0 && currentIndex < newChildrenID.Count)
                        newChildrenID[currentIndex] = newChildrenID[i];
                    newChildrenID[i] = children[i].instanceID;
                }
            }

            childrenID = newChildrenID.ToArray();
            weight = newWeight.ToArray();
        }
    }
}