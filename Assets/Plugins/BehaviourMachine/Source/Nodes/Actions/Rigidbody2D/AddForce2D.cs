//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a force to the "Game Object"'s rigidbody2D.
    /// As a result the rigidbody2D will start moving.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody2D/",
                icon = "ConstantForce",
                description = "Adds a force to the \"Game Object\"\'s rigidbody2D. As a result the rigidbody2D will start moving",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.AddForce.html")]
    public class AddForce2D : ActionNode, IFixedUpdateNode {

        /// <summary>
        /// The game object to add force.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to add force")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The force to be applied.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Dont's Use", tooltip = "The force to be applied. Force.z is ignored")]
        public Vector3Var force;

        /// <summary>
        /// The force in the x axis (overrides Force.x).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The force in the x axis (overrides Force.x)")]
        public FloatVar forceX;

        /// <summary>
        /// The force in the y axis (overrides Force.y).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The force in the y axis (overrides Force.y)")]
        public FloatVar forceY;

        /// <summary>
        /// A value to multiply the Force and change its magnitude.
        /// <summary>
        [VariableInfo(tooltip = "A value to multiply the Force and change its magnitude")]
        public FloatVar multiplyForceBy;

        /// <summary>
        /// If non-zero, clamp the velocity to the given value.
        /// </summary>
        [Tooltip("If non-zero, clamp the resulting velocity to this value")]
        public float maxVelocity = .0f;

        /// <summary>
        /// If true the force will be applied every second; otherwise the force will be applied instantaneously.
        /// </summary>
        [Tooltip("If true the force will be applied every second; otherwise the force will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Rigidbody2D m_Rigidbody2D = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            force = new ConcreteVector3Var();
            forceX = new ConcreteFloatVar();
            forceY = new ConcreteFloatVar();
            multiplyForceBy = 1f;
            maxVelocity = .0f;
            perSecond = true;
        }

        public override Status Update () {
            // Get the rigidbody2D
            if (m_Rigidbody2D == null || m_Rigidbody2D.gameObject != gameObject.Value)
                m_Rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (m_Rigidbody2D == null || multiplyForceBy.isNone)
                return Status.Error;

            // Get the force
            var targetForce = (force.isNone) ? Vector2.zero : new Vector2 (force.Value.x, force.Value.y);
            if (!forceX.isNone) targetForce.x = forceX.Value;
            if (!forceY.isNone) targetForce.y = forceY.Value;

            // Per seconds?
            if (perSecond)
                m_Rigidbody2D.AddForce(this.owner.deltaTime * multiplyForceBy.Value * targetForce);
            else
                m_Rigidbody2D.AddForce(multiplyForceBy.Value * targetForce);

            // Clamped?
            if (maxVelocity > .0f) {
                if (m_Rigidbody2D.velocity.sqrMagnitude > maxVelocity * maxVelocity)
                    m_Rigidbody2D.velocity = m_Rigidbody2D.velocity.normalized * maxVelocity;
            }

            return Status.Success;
        }
    }
}
#endif