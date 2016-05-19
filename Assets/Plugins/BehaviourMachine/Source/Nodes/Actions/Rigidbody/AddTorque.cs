//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Adds a torque to the "Game Object's" rigidbody.
    /// As a result the rigidbody will start spinning around the torque axis.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Adds a torque to the \"Game Object\'s\" rigidbody. As a result the rigidbody will start spinning around the torque axis",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddTorque.html")]
    public class AddTorque : ActionNode, IFixedUpdateNode {

        /// <summary>
        /// The game object to add torque.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to add torque")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Adds the torque relative to the parent (Self) or the World space.
        /// <summary>
        [Tooltip("Adds the torque relative to the parent (Self) or the World space")]
        public Space space = Space.World;

        /// <summary>
        /// The torque to be applied.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Dont's Use", tooltip = "The torque to be applied")]
        public Vector3Var torque;

        /// <summary>
        /// The Torque around x axis (overrides Torque.x).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Override", tooltip = "The Torque around x axis (overrides Torque.x)")]
        public FloatVar torqueX;

        /// <summary>
        /// The Torque around y axis (overrides Torque.y).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Override", tooltip = "The Torque around y axis (overrides Torque.y)")]
        public FloatVar torqueY;

        /// <summary>
        /// The Torque around z axis (overrides Torque.z).
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Override", tooltip = "The Torque around z axis (overrides Torque.z)")]
        public FloatVar torqueZ;

        /// <summary>
        /// A value to multiply the Torque to change its magnitude.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "1", tooltip = "A value to multiply the Torque to change its magnitude")]
        public FloatVar multiplyTorqueBy;

        /// <summary>
        /// The type of the force to apply.
        /// <summary>
        [Tooltip("The type of the force to apply")]
        public ForceMode mode = ForceMode.Force;

        /// <summary>
        /// If true the torque will be applied every second; otherwise the torque will be applied instantaneously.
        /// </summary>
        [Tooltip("If true the torque will be applied every second; otherwise the torque will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            space = Space.World;
            torque = new ConcreteVector3Var();
            torqueX = new ConcreteFloatVar();
            torqueY = new ConcreteFloatVar();
            torqueZ = new ConcreteFloatVar();
            multiplyTorqueBy = new ConcreteFloatVar();
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

            // Get the torque
            var targetTorque = (torque.isNone) ? Vector3.zero : torque.Value;
            if (!torqueX.isNone) targetTorque.x = torqueX.Value;
            if (!torqueY.isNone) targetTorque.y = torqueY.Value;
            if (!torqueZ.isNone) targetTorque.z = torqueZ.Value;

            // Change the torque magnitude?
            if (!multiplyTorqueBy.isNone) targetTorque *= multiplyTorqueBy.Value;

            // Per second?
            if (perSecond)
                targetTorque *= this.owner.deltaTime;

            if (space == Space.World)
                m_Rigidbody.AddTorque(targetTorque, mode);
            else
                m_Rigidbody.AddRelativeTorque(targetTorque, mode);

            return Status.Success;
        }
    }
}
