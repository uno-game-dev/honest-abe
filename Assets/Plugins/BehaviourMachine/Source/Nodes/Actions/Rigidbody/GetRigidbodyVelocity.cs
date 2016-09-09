//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    ///  <summary>
    /// Gets the rigidbody velocity.
    ///  </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Gets the rigidbody velocity",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.html")]
    public class GetRigidbodyVelocity : ActionNode, IFixedUpdateNode {

    	/// <summary>
        /// The game object that has a rigidbody in it.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the velocity.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store the velocity")]
        public Vector3Var velocity;

        /// <summary>
        /// Store the horizontal velocity (Vector3(velocity.x, 0,velocity.z).magnitude).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store the horizontal velocity (Vector3(velocity.x, 0,velocity.z).magnitude)")]
        public FloatVar horizontalVelocity;

        /// <summary>
        /// Store the vertical velocity (velocity.y).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store the vertical velocity (velocity.y)")]
        public FloatVar verticalVelocity;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            velocity = new ConcreteVector3Var();
            horizontalVelocity = new ConcreteFloatVar();
            verticalVelocity = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (m_Rigidbody == null)
                return Status.Error;

            // Get velocity
            var vel = m_Rigidbody.velocity;

            // Store velocity
            if (!velocity.isNone)
                velocity.Value = vel;
            if (!verticalVelocity.isNone)
                verticalVelocity.Value = vel.y;
            if (!horizontalVelocity.isNone) {
                vel.y = 0f;
                horizontalVelocity.Value = vel.magnitude;
            }

            return Status.Success;
        }
    }
}