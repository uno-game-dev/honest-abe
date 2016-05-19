//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the "Game Object" scale (only localScale can be changed).
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Changes the \"Game Object\" scale (only localScale can be changed)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-localScale.html")]
    public class Scale : ActionNode {

    	/// <summary>
        /// The game object to set scale.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to scale")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The amount to scale.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount to scale")]
        public Vector3Var amount;

        /// <summary>
        /// The amount to scale in x axis (overrides amount.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount to scale in x axis (overrides amount.x)")]
        public FloatVar x;

        /// <summary>
        /// The amount to scale in y axis (overrides amount.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount to scale in y axis (overrides amount.y)")]
        public FloatVar y;

        /// <summary>
        /// The amount to scale in z axis (overrides amount.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The amount to scale in z axis (overrides amount.z)")]
        public FloatVar z;

        /// <summary>
        /// If True the scale will be applied every second; otherwise the scale will be applied instantaneously.
        /// </summary>
        [Tooltip("If True the scale will be applied every second; otherwise the scale will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            amount = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            perSecond = true;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if (m_Transform == null)
                return Status.Error;

            // Get the scale
            Vector3 scale = amount.isNone ? Vector3.zero : amount.Value;

            // Override scale values?
            if (!x.isNone) scale.x = x.Value;
            if (!y.isNone) scale.y = y.Value;
            if (!z.isNone) scale.z = z.Value;

            // Scale per second?
            m_Transform.localScale += perSecond ? scale * owner.deltaTime : scale;

            return Status.Success;
        }
    }
}