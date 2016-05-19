//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Rotates the "Object To Rotate" so the forward vector points at "Object To Looked At"'s current position over time.
    /// Returns Error if "Object To Rotate" or "Object To Look At" are None.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Rotates the \"Object To Rotate\" so the forward vector points at \"Object To Looked At\"'s current position over time. Returns Error if \"Object To Rotate\" or \"Object To Look At\" are None")]
    public class LookAtSmooth : ActionNode {

        /// <summary>
        /// The game object to be rotated.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to be rotated")]
        public GameObjectVar objectToRotate;

        /// <summary>
        /// The game object to look at.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to look at")]
        public GameObjectVar objectToLookAt;

        /// <summary>
        /// Then it rotates the transform to point its up direction vector in the direction hinted at by the "World Up" vector.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Vector3.up", tooltip = "Then it rotates the transform to point its up direction vector in the direction hinted at by the \"World Up\" vector")]
        public Vector3Var worldUp;

        /// <summary>
        /// The angular speed in degrees/second to rotate the "Object To Rotate".
        /// </summary>
        [VariableInfo(tooltip = "The angular speed in degrees/second to rotate the \"Object To Rotate\"")]
        public FloatVar speed;

        /// <summary>
        /// If set to True the "Object To Rotate" will only rotate in the y world axis.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "False", tooltip = "If set to True the \"Object To Rotate\" will only rotate in the y world axis")]
        public BoolVar onlyRotateInYAxis;

        [System.NonSerialized]
        Transform m_TransformToRotate = null;
        [System.NonSerialized]
        Transform m_TransformToLookAt = null;

        public override void Reset () {
            objectToRotate = new ConcreteGameObjectVar(this.self);
            objectToLookAt = new ConcreteGameObjectVar(this.self);
            worldUp = new ConcreteVector3Var();
            speed = 120f;
            onlyRotateInYAxis = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the transform that will be rotated
            if (m_TransformToRotate == null || m_TransformToRotate.gameObject != objectToRotate.Value)
                m_TransformToRotate = objectToRotate.Value != null ? objectToRotate.Value.transform : null;

            // Get the position of the object to be looked
            if (m_TransformToLookAt == null || m_TransformToLookAt.gameObject != objectToLookAt.Value)
                m_TransformToLookAt = objectToLookAt.Value != null ? objectToLookAt.Value.transform : null;

            // Validate members
            if (speed.isNone || m_TransformToRotate == null || m_TransformToLookAt == null)
                return Status.Error;

            // Get the look direction
            Vector3 lookDirection = m_TransformToLookAt.position - m_TransformToRotate.position;

            // Rotate only in y-axis?
            if (!onlyRotateInYAxis.isNone && onlyRotateInYAxis.Value) lookDirection.y = 0; lookDirection.Normalize();

            // Rotate
            if (lookDirection != Vector3.zero) {
                Quaternion _desiredRotation = Quaternion.LookRotation(lookDirection, (worldUp.isNone) ? Vector3.up : worldUp.Value);
                m_TransformToRotate.rotation = Quaternion.Slerp(m_TransformToRotate.rotation, _desiredRotation, this.owner.deltaTime * speed.Value * Mathf.Deg2Rad);
            }

            return Status.Success;
        }
    }
}