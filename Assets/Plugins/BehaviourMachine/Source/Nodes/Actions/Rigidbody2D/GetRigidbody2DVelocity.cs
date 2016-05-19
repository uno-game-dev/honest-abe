//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    [NodeInfo ( category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Gets the rigidbody2D velocity",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.html")]
    public class GetRigidbody2DVelocity : ActionNode {

    	/// <summary>
        /// The game object that has a rigidbody2D in it.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody2D in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the velocity.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store the velocity. Velocity.z is ignored")]
        public Vector3Var velocity;

        /// <summary>
        /// Store the horizontal velocity (velocity.x).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store the horizontal velocity (velocity.x)")]
        public FloatVar horizontalVelocity;

        /// <summary>
        /// Store the vertical velocity (velocity.y).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store the vertical velocity (velocity.y)")]
        public FloatVar verticalVelocity;

        [System.NonSerialized]
        Rigidbody2D m_Rigidbody2D = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            velocity = new ConcreteVector3Var();
            horizontalVelocity = new ConcreteFloatVar();
            verticalVelocity = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the rigidbody2D
            if (m_Rigidbody2D == null || m_Rigidbody2D.gameObject != gameObject.Value)
                m_Rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (m_Rigidbody2D == null)
                return Status.Error;

            // Get velocity
            var vel = m_Rigidbody2D.velocity;

            // Store velocity
            if (!velocity.isNone)
                velocity.vector2Value = vel;
            if (!verticalVelocity.isNone)
                verticalVelocity.Value = vel.y;
            if (!horizontalVelocity.isNone)
                horizontalVelocity.Value = vel.x;

            return Status.Success;
        }
    }
}
#endif