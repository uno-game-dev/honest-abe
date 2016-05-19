//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Applies a rotation of eulerAngles.z degrees around the z axis, eulerAngles.x degrees around the x axis, and eulerAngles.y degrees around the y axis (in that order).
    /// <seealso cref="Action.Transform.Rotate" />
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Applies a rotation of eulerAngles.z degrees around the z axis, eulerAngles.x degrees around the x axis, and eulerAngles.y degrees around the y axis (in that order)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.MoveRotation.html")]
    public class MoveRotation : ActionNode, IFixedUpdateNode {

    	/// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        [VariableInfo(requiredField = false, nullLabel = "Don't Use",tooltip = "The eulerAngles to rotate the rigidbody")]
        public Vector3Var eulerAngles;

        /// <summary>
        /// Euler Angles in the x axis (overrides eulerAngles.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Euler Angles in the x axis (overrides eulerAngles.x)")]
        public FloatVar x;

        /// <summary>
        /// Euler Angles in the y axis (overrides eulerAngles.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Euler Angles in the y axis (overrides eulerAngles.y)")]
        public FloatVar y;

        /// <summary>
        /// Euler Angles in the z axis (overrides eulerAngles.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Euler Angles in the z axis (overrides eulerAngles.z)")]
        public FloatVar z;

        /// <summary>
        /// Self, applies the rotation around the transform's local axes. World, applies the rotation around the global axes.
        /// </summary>
        [Tooltip("Self, applies the rotation around the transform's local axes. World, applies the rotation around the global axes")]
        public Space relativeTo = Space.Self;

        /// <summary>
        /// If True the object will be rotated every second; otherwise the object will be rotated instantaneously.
        /// </summary>
        [Tooltip("If True the object will be rotated every second; otherwise the object will be rotated instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            eulerAngles = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            perSecond = true;
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (m_Rigidbody == null)
                return Status.Error;

            // Get the velocity
            Vector3 desiredAngle =  !eulerAngles.isNone ? eulerAngles.Value : Vector3.zero;

            // Override values?
            if (!x.isNone) desiredAngle.x = x.Value;
            if (!y.isNone) desiredAngle.y = y.Value;
            if (!z.isNone) desiredAngle.z = z.Value;

            // Get relativeDirection
            Vector3 relativeAngle = relativeTo == Space.Self ? desiredAngle : m_Rigidbody.transform.InverseTransformDirection(desiredAngle);

            // Rotate per second?
            if (perSecond)
                m_Rigidbody.MoveRotation (m_Rigidbody.rotation * Quaternion.Euler(relativeAngle * this.owner.deltaTime));
            else
                m_Rigidbody.MoveRotation (m_Rigidbody.rotation * Quaternion.Euler(relativeAngle));

            return Status.Success;
        }
    }
}