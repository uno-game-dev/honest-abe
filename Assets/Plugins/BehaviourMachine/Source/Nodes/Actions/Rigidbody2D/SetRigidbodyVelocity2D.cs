//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the rigidbody2D velocity.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Sets the rigidbody2D velocity",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D-velocity.html")]
    public class SetRigidbody2DVelocity : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody2D in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody2D in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new velocity (ignores "New Velocity".z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new velocity (ignores \"New Velocity\".z)")]
        public Vector3Var newVelocity;

        /// <summary>
        /// The velocity in the x axis (overrides "New Velocity".x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Velocity in the x axis (overrides \"New Velocity\".x)")]
        public FloatVar horizontalVelocity;

        /// <summary>
        /// The velocity in the y axis (overrides "New Velocity".y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The velocity in the y axis (overrides \"New Velocity\".y)")]
        public FloatVar verticalVelocity;

        [System.NonSerialized]
        Rigidbody2D m_Rigidbody2D = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newVelocity = new ConcreteVector3Var();
            horizontalVelocity = new ConcreteFloatVar();
            verticalVelocity = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the rigidbody2D
            if (m_Rigidbody2D == null || m_Rigidbody2D.gameObject != gameObject.Value)
                m_Rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members?
            if  (m_Rigidbody2D == null)
                return Status.Error;

            // Get velocity
            Vector2 velocity = newVelocity.isNone ? Vector2.zero : new Vector2(newVelocity.Value.x, newVelocity.Value.y);

            // Override values?
            if (!horizontalVelocity.isNone) velocity.x = horizontalVelocity.Value;
            if (!verticalVelocity.isNone) velocity.y = verticalVelocity.Value;

            // Set velocity
            m_Rigidbody2D.velocity = velocity;

            return Status.Success;
        }
    }
}
#endif