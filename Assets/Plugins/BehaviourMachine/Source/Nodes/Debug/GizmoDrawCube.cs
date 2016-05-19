//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
	/// <summary>
	/// Draws a solid cube (Editor Only).
	/// </summary>
	[NodeInfo ( category = "Debug/",
				icon = "Gizmos",
				description = "Draws a solid cube (Editor Only)")]
	public class GizmoDrawCube : ActionNode {

		public enum CubeType {Wired, Solid};

		/// <summary>
        /// The target object to draw the sphere.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The target object to draw the sphere")]
        public GameObjectVar gameObject;

		/// <summary>
		/// The center of the sphere.
		/// <summary>
		[VariableInfo(tooltip = "The center of the sphere")]
		public Vector3Var center;

		/// <summary>
		/// The size of the sphere.
		/// <summary>
		[VariableInfo(tooltip = "The size of the sphere")]
		public Vector3Var size;

		/// <summary>
		/// The color of the sphere.
		/// <summary>
		[VariableInfo(tooltip = "The color of the sphere")]
		public ColorVar color;

		public CubeType cubeType;

		[System.NonSerialized]
        Transform m_Transform = null;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			center = Vector3.zero;
			size = new Vector3(1f, 1f, 1f);
			color = Color.white * .5f;
			cubeType = CubeType.Wired;
		}

		public override void Start () {
	  		tree.blackboard.onDrawGizmos += OnDrawGizmos;
	  	}

	    public override Status Update () {
	    	// Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if (m_Transform == null || size.isNone || center.isNone)
                return Status.Error;

	        return Status.Running;
	    }

	  	public override void End () {
	  		tree.blackboard.onDrawGizmos -= OnDrawGizmos;
	  	}

	    void OnDrawGizmos () {
	    	if (this.status == Status.Running && m_Transform != null) {
	    		Gizmos.color = color.Value;
	    		if (cubeType == CubeType.Solid)
	    			Gizmos.DrawCube(m_Transform.position + center.Value, size.Value);
	    		else
	    			Gizmos.DrawWireCube(m_Transform.position + center.Value, size.Value);
	    	}
	    }
	}
}