//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary> 
    /// A base class for branch nodes in a BehaviourTree.
    /// A branch can have children nodes.
    /// </summary>
    public abstract class BranchNode : ActionNode {

        #region Properties
        /// <summary> 
        /// Returns all nodes in this branch.
        /// </summary>
        public abstract ActionNode[] children {get;}
        #endregion Properties

        
        #region Protected Methods
        /// <summary> 
        /// Returns True if the supplied branch is in an ancestor of the node.
        /// <param name="branch">The branch to test is an ancestor</param>
        /// <returns>True if the node is in the supplied branch hierarchy; False otherwise.</returns>
        /// </summary>
        protected bool IsAncestor (BranchNode branch) {
            // The branch is not a grandfather
            for  (var grandfather = this.branch; grandfather != null; grandfather = grandfather.branch) {
                if (grandfather == branch)
                    return true;
            }
            return false;
        }
        #endregion Protected Methods

        
        #region Public Methods
        /// <summary>
        /// Returns the branch hierarchy nodes.
        /// <returns>Tha branch node and all children/subchildren.<returns>
        /// </summary>
         public ActionNode[] GetHierarchy () {
            List<ActionNode> allNodes = new List<ActionNode>() {this};

            foreach (ActionNode child in this.children) {
                BranchNode childBranch = child as BranchNode;
                if (childBranch != null)
                    allNodes.AddRange(childBranch.GetHierarchy());
                else
                    allNodes.Add(child);
            }

            return allNodes.ToArray();
        }

        /// <summary> 
        /// Returns true if the supplied node can be add as child of the branch.
        /// <param name="newChild">The node to be tested if it can be a child of the branch.</param>
        /// <returns>True if the newChild can be a child of the branch; otherwise false.</returns>
        /// </summary>
        public virtual bool CanAddNode (ActionNode newChild) {
            return newChild != null && newChild.tree == this.tree && !(newChild is FunctionNode) && (!(newChild is BranchNode) || !IsAncestor(newChild as BranchNode));
        }

        /// <summary> 
        /// Set the children in the branch.
        /// Used during the node's serialization.
        /// <param name="newChildren">The new child nodes.</param>
        /// <returns>True if the new children was successfully added; false otherwise.</returns>
        /// <seealso cref="BehaviourMachine.NodeSerialization" />
        /// </summary>
        public abstract bool SetChildren (ActionNode[] newChildren);

        /// <summary>
        /// Returns a copy of this node.
        /// <param name = "newOwner">The owner of the new node.</param>
        /// <returns>The copy of the node.</returns>
        /// </summary>
        public override ActionNode Copy (INodeOwner newOwner) {
            // Copy node
            var copy = base.Copy(newOwner);
            var branchCopy = copy as BranchNode;

            // The copy is a valid branch?
            if (branchCopy != null) {
                // Copy children
                var copiedChildren = new List<ActionNode>();
                for (int i = 0; i < this.children.Length; i++) {
                    var copiedChild = this.children[i].Copy(newOwner);
                    if (copiedChild != null)
                        copiedChildren.Add(copiedChild);
                }

                branchCopy.SetChildren(copiedChildren.ToArray());
            }

            return copy;
        }

        /// <summary> 
        /// Adds the node to the branch nodes list.
        /// <param name = "child">The node to be added.</param>
        /// <returns>True if the node was added to the list; otherwise false.</returns>
        /// </summary>
        public abstract bool Add (ActionNode child);

        /// <summary> 
        /// Inserts a node in the nodes list at the supplied index.
        /// <param name = "index">The index to insert the behaviour.</param>
        /// <param name = "child">The node to be added to the list.</param>
        /// <returns>True if the node was added to the list; otherwise false.</returns>
        /// </summary>
        public abstract bool Insert (int index, ActionNode child);

        /// <summary> 
        /// Removes the supplied node from this branch.
        /// <param name = "child">The object to be removed from the list.</param>
        /// </summary>
        public abstract void Remove (ActionNode child);

        /// <summary> 
        /// Determines whether a node is a child of this branch.
        /// <param name = "child">The object to locate in the children list.</param>
        /// <returns>True if node is found in the children list; otherwise, false.</returns>
        /// </summary>
        public abstract bool Contains (ActionNode child);
        #endregion Public Methods
    }
}