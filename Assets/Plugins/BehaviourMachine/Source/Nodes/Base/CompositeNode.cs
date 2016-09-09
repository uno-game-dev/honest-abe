//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary> 
    /// A base class for composite nodes in a BehaviourTree.
    /// A composite node is a branch that can have more than one child.
    /// </summary>
    public abstract class CompositeNode : BranchNode {

        #region Members
        [System.NonSerialized]
        ActionNode[] m_Children = new ActionNode[0];
        #endregion Members

        #region Properties
        /// <summary> 
        /// Returns all child nodes.
        /// </summary>
        public override ActionNode[] children {
            get {return m_Children;}
        }
        #endregion Properties

        #region Public Methods
        /// <summary> 
        /// Set the children in the branch.
        /// Used during the node's serialization.
        /// <param name="newChildren">The new child nodes.</param>
        /// <returns>True if the new children was successfully added; false otherwise.</returns>
        /// <seealso cref="BehaviourMachine.NodeSerialization" />
        /// </summary>
        public override bool SetChildren (ActionNode[] newChildren) {
            // Validate children
            for (int i = 0; i < newChildren.Length; i++) {
                if (!CanAddNode(newChildren[i]))
                    return false;
            }

            // Set branch
            for (int i = 0; i < newChildren.Length; i++)
                newChildren[i].branch = this;

            m_Children = newChildren;
            return true;
        }


        /// <summary> 
        /// Adds the node to the branch nodes list.
        /// <param name = "child">The node to be added to the list.</param>
        /// <returns>True if the node was added to the list; otherwise false.</returns>
        /// </summary>
        public override bool Add (ActionNode child) {
            if (CanAddNode(child) && !Contains(child)) {
                // Remove from old branch
                if (child.branch != null)
                    child.branch.Remove(child);

                // Add the child to this branch
                var newChildren = new List<ActionNode>(m_Children);
                newChildren.Add(child);
                m_Children = newChildren.ToArray();
                child.branch = this;

                return true;
            }
            return false;
        }

        /// <summary> 
        /// Inserts a node in the nodes list at the supplied index.
        /// <param name = "index">The index to insert the behaviour.</param>
        /// <param name = "child">The node to be added to the list.</param>
        /// <returns>True if the node was added to the list; otherwise false.</returns>
        /// </summary>
        public override bool Insert (int index, ActionNode child) {
            if (child != null && index >= 0 && index <= m_Children.Length) {
                if (!Contains(child) && !Add(child))
                    return false;

                // Insert child
                var newChildren = new List<ActionNode>(m_Children);
                newChildren.Remove(child);
                newChildren.Insert(index, child);
                m_Children = newChildren.ToArray();

                return true;
            }

            return false;
        }

        /// <summary> 
        /// Removes the node from this branch.
        /// <param name = "child">The object to be removed from the list.</param>
        /// </summary>
        public override void Remove (ActionNode child) {
            if (Contains(child)) {
                // Remove child
                var newChildren = new List<ActionNode>(m_Children);
                newChildren.Remove(child);
                m_Children = newChildren.ToArray();
                if (child != null && child.branch == this)
                    child.branch = null;

                // Call ResetStatus if it is playing
                if (Application.isPlaying)
                    child.ResetStatus();
            }
        }

        /// <summary> 
        /// Determines whether a node is a child of this branch.
        /// <param name = "child">The object to locate in the children list.</param>
        /// <returns>True if node is found in the children list; otherwise, false.</returns>
        /// </summary>
        public override bool Contains (ActionNode child) {
            for (int i = 0; i < m_Children.Length; i++) {
                if (m_Children[i] == child)
                    return true;
            }
            return false;
        }

        /// <summary> 
        /// Call OnTick in every child.
        /// Returns the last child status.
        /// Returns Error if the children list is empty.
        /// </summary>
        public override Status Update () {
            var childStatus = Status.Error;

            for (int i = 0; i < m_Children.Length; i++) {
                m_Children[i].OnTick();
                childStatus = m_Children[i].status;
                if (childStatus == Status.Error || childStatus == Status.Running)
                    break;
            }

            return childStatus;
        }

        /// <summary> 
        /// Call EditorOnTick in every child.
        /// Returns the last child status.
        /// Returns Error if the children list is empty.
        /// </summary>
        public override void EditorOnTick () {
            // var childStatus = Status.Error;

            for (int i = 0; i < m_Children.Length; i++) {
                m_Children[i].EditorOnTick();
                // childStatus = m_Children[i].status;
                // if (childStatus == Status.Error || childStatus == Status.Running)
                //     break;
            }

            // this.status = childStatus;
        }

        /// <summary>
        /// Call ResetStatus in children.
        /// <summary>
        public override void ResetStatus () {
            // Call base ResetStatus
            base.ResetStatus();

            // Call ResetStatus in children.
            foreach (ActionNode child in children)
                child.ResetStatus();
        }
        #endregion Public Methods
    }
}