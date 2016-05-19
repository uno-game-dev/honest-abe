//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
	/// <summary>
	/// Draws a line starting at from towards to.
	/// </summary>
	[NodeInfo ( category = "Debug/",
                icon = "Gizmos",
                description = "Draws a line starting at from towards to")]
	public class GizmoDrawLine : ActionNode {

		/// <summary>
        /// The start position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The start position")]
        public GameObjectVar from;

		/// <summary>
		/// The end position.
		/// <summary>
		[VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The end position")]
		public GameObjectVar to;

		/// <summary>
		/// The color of the sphere.
		/// <summary>
		[VariableInfo(tooltip = "The color of the sphere")]
		public ColorVar color;


		[System.NonSerialized]
        Transform m_TransformFrom = null;
        [System.NonSerialized]
        Transform m_TransformTo = null;

		public override void Reset () {
			from = new ConcreteGameObjectVar(this.self);
			to = new ConcreteGameObjectVar(this.self);
			color = Color.red;
		}

	    public override Status Update () {
	    	// Get the from transform
            if (m_TransformFrom == null || m_TransformFrom.gameObject != from.Value)
                m_TransformFrom = from.Value != null ? from.Value.transform : null;

            // Get the to transform
            if (m_TransformTo == null || m_TransformTo.gameObject != to.Value)
                m_TransformTo = to.Value != null ? to.Value.transform : null;

            // Validate members
            if (m_TransformFrom == null || m_TransformTo == null)
                return Status.Error;

	        return Status.Running;
	    }

	  	public override void Start () {
	  		tree.blackboard.onDrawGizmos += OnDrawGizmos;
	  	}

	  	public override void End () {
	  		tree.blackboard.onDrawGizmos -= OnDrawGizmos;
	  	}

	    void OnDrawGizmos () {
	    	if (this.status == Status.Running && m_TransformFrom != null && m_TransformTo != null) {
	    		Gizmos.color = color.Value;
	    		Gizmos.DrawLine(m_TransformFrom.position, m_TransformTo.position);
	    	}
	    }
	}
}