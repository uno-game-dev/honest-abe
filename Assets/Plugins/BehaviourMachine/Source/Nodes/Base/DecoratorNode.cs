//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    /// <summary> 
    /// Base class for decorator nodes.
    /// The DecoratorNode changes its child execution in some way.
    /// </summary>
    public abstract class DecoratorNode : BranchNode {

        #region Members
        [System.NonSerialized]
    	ActionNode m_Child;
        #endregion Members

        #region Properties
    	/// <summary> 
        /// Returns the child node.
        /// </summary>
		public ActionNode child {get {return m_Child;}}

		/// <summary> 
        /// Returns an array containing the child; if the child is null returns an empty array.
        /// </summary>
		public override ActionNode[] children {
            get {return m_Child != null ? new ActionNode[] {m_Child} : new ActionNode[0];}
        }
        #endregion Properties

        #region Public Methods 
        public override bool CanAddNode (ActionNode child) {
            return base.CanAddNode(child) && m_Child == null;
        }

        /// <summary> 
        /// Set the children in the branch.
        /// Used during the node's serialization.
        /// <param name="newChildren">The new child nodes.</param>
        /// <returns>True if the new children were successfully added; false otherwise.</returns>
        /// <seealso cref="BehaviourMachine.NodeSerialization" />
        /// </summary>
        public override bool SetChildren (ActionNode[] newChildren) {
            if (newChildren.Length == 1 && CanAddNode(newChildren[0])) {
                m_Child = newChildren[0];
                m_Child.branch = this;
                return true;
            }
            else if (newChildren.Length <= 0) {
                if (m_Child != null)
                    m_Child.branch = null;
                m_Child = null;
                return true;
            }
            return false;
        }

		/// <summary> 
        /// Adds a child node to the branch.
        /// <param name = "child">The node to be added to the branch.</param>
        /// <returns>True if the node was added to the list; otherwise false.</returns>
        /// </summary>
        public override bool Add (ActionNode child) {
            if (CanAddNode(child)) {
                m_Child = child;
                m_Child.branch = this;
                return true;
            }
            return false;
        }

        /// <summary> 
        /// Inserts a node in the branch list at the supplied index.
        /// <param name = "index">The index to insert the behaviour.</param>
        /// <param name = "child">The node to be added to the list.</param>
        /// <returns>True if the node was added to the list; otherwise false.</returns>
        /// </summary>
        public override bool Insert (int index, ActionNode child) {
        	if (index != 0)
        		return false;
        	return Add (child);
        }

        /// <summary> 
        /// Removes a node from the branch.
        /// <param name = "child">The node to be removed from the list.</param>
        /// </summary>
        public override void Remove (ActionNode child) {
        	if (m_Child == child) {
                m_Child = null;

        		if (child != null && child.branch == this)
        			child.branch = null;
        	}
        }

        /// <summary> 
        /// Determines whether a node is a child of this branch.
        /// <param name = "child">The node to locate in the children list.</param>
        /// <returns>True if node is found in the children list; otherwise, false.</returns>
        /// </summary>
        public override bool Contains (ActionNode child) {
        	return m_Child == child;
        }

        /// <summary> 
        /// Call OnTick in child.
        /// Returns Error if the child is null; otherwise returns Success.
        /// </summary>
        public override Status Update () {            
            if (m_Child == null)
                return Status.Error;

            child.OnTick();
            return child.status;
        }

        /// <summary>
        /// Call ResetStatus in child.
        /// <summary>
        public override void ResetStatus () {
            // Call base ResetStatus
            base.ResetStatus();

            // Call ResetStatus in child.
            if (m_Child != null)
                m_Child.ResetStatus();
        }
        #endregion Public Methods         	
    }
}