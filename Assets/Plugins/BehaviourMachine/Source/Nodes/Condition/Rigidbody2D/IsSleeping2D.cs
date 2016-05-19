//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if the rigidibody in the "Game Object" is sleeping; otherwise returns Failure.
    /// </summary>
    [NodeInfo ( category = "Condition/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Returns Success if the rigidibody in the \"Game Object\" is sleeping; otherwise returns Failure")]
    public class IsSleeping2D : ConditionNode {

    	/// <summary>
        /// The game object that has a rigidbody2D in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody2D in it")]
        public GameObjectVar gameObject;

        [System.NonSerialized]
        Rigidbody2D m_Rigidbody2D = null;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the rigidbody2D
            if (m_Rigidbody2D == null || m_Rigidbody2D.gameObject != gameObject.Value)
                m_Rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (m_Rigidbody2D == null)
                return Status.Error;

            if (m_Rigidbody2D.IsSleeping()) {
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
#endif