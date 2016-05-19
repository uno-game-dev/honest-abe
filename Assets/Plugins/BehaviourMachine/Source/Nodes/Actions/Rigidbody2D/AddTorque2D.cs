//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a torque to the "Game Object's" rigidbody2D.
    /// As a result the rigidbody2D will start spinning around the torque axis.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Adds a torque to the \"Game Object\'s\" rigidbody2D. As a result the rigidbody2D will start spinning around the torque axis",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.AddTorque.html")]
    public class AddTorque2D : ActionNode, IFixedUpdateNode {

        /// <summary>
        /// The game object to add torque.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to add torque")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The torque to be applied.
        /// <summary>
        [VariableInfo(tooltip = "The torque to be applied")]
        public FloatVar torque;

        /// <summary>
        /// A value to multiply the Torque to change its magnitude.
        /// <summary>
        [VariableInfo(tooltip = "A value to multiply the \"Torque\" to change its magnitude")]
        public FloatVar multiplyTorqueBy;

        /// <summary>
        /// If true the torque will be applied every second; otherwise the torque will be applied instantaneously.
        /// </summary>
        [Tooltip("If true the torque will be applied every second; otherwise the torque will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Rigidbody2D m_Rigidbody2D = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            torque = new ConcreteFloatVar();
            multiplyTorqueBy = 1f;
            perSecond = true;
        }

        public override Status Update () {
            // Get the rigidbody2D
            if (m_Rigidbody2D == null || m_Rigidbody2D.gameObject != gameObject.Value)
                m_Rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (m_Rigidbody2D == null || torque.isNone || multiplyTorqueBy.isNone)
                return Status.Error;

            // Per second?
            if (perSecond) {
                // Apply torque
                m_Rigidbody2D.AddTorque(this.owner.deltaTime * multiplyTorqueBy.Value * torque.Value);
            }
            else {
                // Apply torque
                m_Rigidbody2D.AddTorque(multiplyTorqueBy.Value * torque.Value);
            }

            return Status.Success;
        }
    }
}
#endif