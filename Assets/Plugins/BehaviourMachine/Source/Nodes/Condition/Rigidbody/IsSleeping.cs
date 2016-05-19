//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
	/// Returns Success if the rigidibody in the "Game Object" is sleeping; otherwise returns Failure.
	/// </summary>
    [NodeInfo ( category = "Condition/Rigidbody/",
                icon = "Rigidbody",
                description = "Returns Success if the rigidibody in the \"Game Object\" is sleeping; otherwise returns Failure")]
    public class IsSleeping : ConditionNode {

    	/// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (m_Rigidbody == null)
                return Status.Error;

            if (m_Rigidbody.IsSleeping()) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}