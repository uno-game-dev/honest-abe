//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a force to the "Game Object"'s rigidbody.
    /// As a result the rigidbody will start moving.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody/",
                icon = "ConstantForce",
                description = "Adds a force to the \"Game Object\"\'s rigidbody. As a result the rigidbody will start moving",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddForce.html")]
    public class AddForce : ActionNode, IFixedUpdateNode {

        /// <summary>
        /// The game object to add force.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to add force")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Adds the force relative to the parent (Self) or the World space.
        /// <summary>
        [Tooltip("Adds the force relative to the parent (Self) or the World space")]
        public Space space = Space.World;

        /// <summary>
        /// The force to be applied.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Dont's Use", tooltip = "The force to be applied")]
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
        /// The force in the z axis (overrides Force.z).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The force in the z axis (overrides Force.z)")]
        public FloatVar forceZ;

        /// <summary>
        /// A value to multiply the Force and change its magnitude.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "1", tooltip = "A value to multiply the Force and change its magnitude")]
        public FloatVar multiplyForceBy;

        /// <summary>
        /// If non-zero, clamp the velocity to the given value.
        /// </summary>
        [Tooltip("If non-zero, clamp the resulting velocity to this value")]
        public float maxVelocity = .0f;

        /// <summary>
        /// The type of the force to apply.
        /// <summary>
        [Tooltip("The type of the force to apply")]
        public ForceMode mode;

        /// <summary>
        /// If true the force will be applied every second; otherwise the force will be applied instantaneously.
        /// </summary>
        [Tooltip("If true the force will be applied every second; otherwise the force will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            space = Space.World;
            force = new ConcreteVector3Var();
            forceX = new ConcreteFloatVar();
            forceY = new ConcreteFloatVar();
            forceZ = new ConcreteFloatVar();
            multiplyForceBy = new ConcreteFloatVar();
            maxVelocity = .0f;
            mode = ForceMode.Force;
            perSecond = true;
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (m_Rigidbody == null)
                return Status.Error;

            // Get the force
            var targetForce = (force.isNone) ? Vector3.zero : force.Value;
            if (!forceX.isNone) targetForce.x = forceX.Value;
            if (!forceY.isNone) targetForce.y = forceY.Value;
            if (!forceZ.isNone) targetForce.z = forceZ.Value;

            // Change the force magnitude?
            if (!multiplyForceBy.isNone) targetForce *= multiplyForceBy.Value;

            // Per second?
            if (perSecond)
                targetForce *= owner.deltaTime;

            if (space == Space.World)
                m_Rigidbody.AddForce(targetForce, mode);
            else
                m_Rigidbody.AddRelativeForce(targetForce, mode);

            // Clamped?
            if (maxVelocity > .0f) {
                if (m_Rigidbody.velocity.sqrMagnitude > maxVelocity * maxVelocity)
                    m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * maxVelocity;
            }

            return Status.Success;
        }
    }
}