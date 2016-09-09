//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Applies a rotation of eulerAngles.z degrees around the z axis, eulerAngles.x degrees around the x axis, and eulerAngles.y degrees around the y axis (in that order).
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Applies a rotation of eulerAngles.z degrees around the z axis, eulerAngles.x degrees around the x axis, and eulerAngles.y degrees around the y axis (in that order)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Rotate.html")]
    public class Rotate : ActionNode {

        /// <summary>
        /// The game object to rotate.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to rotate")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The amount to rotate in euler angles.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount to rotate in Euler Angles")]
        public Vector3Var eulerAngles;

        /// <summary>
        /// The amount (in degrees) to rotate in x axis (overrides eulerAngles.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount (in degrees) to rotate in x axis (overrides eulerAngles.x)")]
        public FloatVar x;

        /// <summary>
        /// The amount (in degrees) to rotate in y axis (overrides eulerAngles.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount (in degrees) to rotate in y axis (overrides eulerAngles.y)")]
        public FloatVar y;

        /// <summary>
        /// The amount (in degrees) to rotate in z axis (overrides eulerAngles.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount (in degrees) to rotate in z axis (overrides eulerAngles.z)")]
        public FloatVar z;

        /// <summary>
        /// Self, applies the rotation around the transform's local axes. World, applies the rotation around the global axes.
        /// </summary>
        [Tooltip("Self, applies the rotation around the transform's local axes. World, applies the rotation around the global axes")]
        public Space relatveTo = Space.Self;

        /// <summary>
        /// If True Should the rotation will be applied every second; otherwise the rotation will be applied instantaneously.
        /// </summary>
        [Tooltip("If True the rotation will be applied every second; otherwise the rotation will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            eulerAngles = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            relatveTo = Space.Self;
            perSecond = true;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if (m_Transform == null)
                return Status.Error;

            // Get the angles
            Vector3 angles = eulerAngles.isNone ? Vector3.zero : eulerAngles.Value;

            // Override angle values?
            if (!x.isNone) angles.x = x.Value;
            if (!y.isNone) angles.y = y.Value;
            if (!z.isNone) angles.z = z.Value;

            // Rotate per second?
            if (perSecond)
                m_Transform.Rotate(angles * owner.deltaTime, relatveTo);
            else
                m_Transform.Rotate(angles, relatveTo);

            return Status.Success;
        }
    }
}