//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the rigidbody velocity.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Sets the rigidbody velocity",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody-velocity.html")]
    public class SetRigidbodyVelocity : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new velocity.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new velocity")]
        public Vector3Var newVelocity;

        /// <summary>
        /// The velocity in the x axis (overrides "New Velocity".x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Velocity in the x axis (overrides \"New Velocity\".x)")]
        public FloatVar x;

        /// <summary>
        /// The velocity in the y axis (overrides "New Velocity".y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The velocity in the y axis (overrides \"New Velocity\".y)")]
        public FloatVar y;

        /// <summary>
        /// The velocity in the z axis (overrides "New Velocity".z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The velocity in the z axis (overrides \"New Velocity\".z)")]
        public FloatVar z;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newVelocity = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members?
            if  (m_Rigidbody == null)
                return Status.Error;

            // Get velocity
            Vector3 velocity = newVelocity.isNone ? Vector3.zero : newVelocity.Value;

            // Override values?
            if (!x.isNone) velocity.x = x.Value;
            if (!y.isNone) velocity.y = y.Value;
            if (!z.isNone) velocity.z = z.Value;

            // Set velocity
            m_Rigidbody.velocity = velocity;

            return Status.Success;
        }
    }
}