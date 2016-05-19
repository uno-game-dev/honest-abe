//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Rotates the game object using the mouse.
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Rotates the game object using the mouse")]
	public class MouseLook : ActionNode {

		/// <summary>
        /// The game object to be rotated.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to be rotated")]
        public GameObjectVar objectToRotate;

        /// <summary>
        /// Ignore the x mouse movement?
        /// </summary>
        [Tooltip("Ignore the x mouse movement?")]
		public bool ignoreX;

		/// <summary>
        /// Ignore the y mouse movement?
        /// </summary>
		[Tooltip("Ignore the y mouse movement?")]
		public bool ignoreY;

		/// <summary>
        /// Invert the rotation in the x axis?
        /// </summary>
		[VariableInfo(requiredField = false, nullLabel = "False", tooltip = "Invert the rotation in the x axis?")]
		public BoolVar invertX;

		/// <summary>
        /// Invert the rotation in the y axis?
        /// </summary>
		[VariableInfo(requiredField = false, nullLabel = "False", tooltip = "Invert the rotation in the y axis?")]
		public BoolVar invertY;

		/// <summary>
        /// The sensitiveness in the x axis.
        /// </summary>
		[VariableInfo(tooltip = "The sensitiveness in the x axis.")]
		public FloatVar sensitiveX;

		/// <summary>
        /// The sensitiveness in the y axis.
        /// </summary>
		[VariableInfo(tooltip = "The sensitiveness in the y axis.")]
		public FloatVar sensitiveY;

		/// <summary>
        /// The minimum rotation in the x axis.
        /// </summary>
		[VariableInfo(tooltip = "The minimum rotation in the x axis")]
		public FloatVar minX;

		/// <summary>
        /// The maximum rotation in the x axis.
        /// </summary>
		[VariableInfo(tooltip = "The maximum rotation in the x axis")]
		public FloatVar maxX;

		/// <summary>
        /// The minimum rotation in the y axis.
        /// </summary>
		[VariableInfo(tooltip = "The minimum rotation in the y axis")]
		public FloatVar minY;

		/// <summary>
        /// The maximum rotation in the y axis.
        /// </summary>
		[VariableInfo(tooltip = "The maximum rotation in the y axis")]
		public FloatVar maxY;

		[System.NonSerialized]
        Transform m_TransformToRotate = null;

		public override void Reset () {
			objectToRotate = new ConcreteGameObjectVar(this.self);
			ignoreX = false;
			ignoreY = false;
			invertX = new ConcreteBoolVar();
			invertY = new ConcreteBoolVar();
			sensitiveX = 1f;
			sensitiveY = 1f;
			minX = -360f;
			maxX = 360f;
			minY = -360f;
			maxY = 360f;
		}

		public override Status Update () {
			// Get the transformToRotate
            if (m_TransformToRotate == null || m_TransformToRotate.gameObject != objectToRotate.Value)
                m_TransformToRotate = objectToRotate.Value != null ? objectToRotate.Value.transform : null;

            // Validate members
            if (m_TransformToRotate == null)
            	return Status.Error;

            Vector3 angle = m_TransformToRotate.localEulerAngles;

            if (!ignoreX) {
            	angle.x += (invertX.Value ? (-1f * Input.GetAxis("Mouse Y")) : Input.GetAxis("Mouse Y")) * sensitiveX.Value;
				angle.x = Mathf.Clamp(angle.x, minX.Value, maxX.Value);
            }

            if (!ignoreY) {
            	angle.y += (invertY.Value ? (-1f * Input.GetAxis("Mouse X")) : Input.GetAxis("Mouse X")) * sensitiveY.Value;
				angle.y = Mathf.Clamp(angle.y, minY.Value, maxY.Value);
            }

            m_TransformToRotate.localEulerAngles = angle;
            return Status.Success;
		}
	}
}