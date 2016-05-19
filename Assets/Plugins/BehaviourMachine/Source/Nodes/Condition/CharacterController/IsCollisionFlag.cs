//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace  BehaviourMachine {

    /// <summary>
    /// Returns Success if the character controller in the "Game Object" has "Collision Flag"; otherwise returns Failure. Only works if you are using Move or SimpleMove to move the character controller.
    /// </summary>
    [NodeInfo ( category = "Condition/CharacterController/",
                icon = "CharacterController",
                description = "Returns Success if the character controller in the \"Game Object\" has the supplied \"Collision Flag\"; otherwise returns Failure. Only works if you are using Move or SimpleMove to move the character controller")]
    public class IsCollisionFlag : ConditionNode {

        /// <summary>
        /// The game object that has a Character Controller in it.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a Character Controller in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// What part of the capsule collided with the environment.
        /// <summary>
        [Tooltip("What part of the capsule collided with the environment")]
        public CollisionFlags collisionFlag;

        [System.NonSerialized]
        CharacterController m_Controller = null;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            collisionFlag = CollisionFlags.None;
        }

        public override Status Update () {
            // Get the controller
            if (m_Controller == null || m_Controller.gameObject != gameObject.Value)
                m_Controller = gameObject.Value != null ? gameObject.Value.GetComponent<CharacterController>() : null;

            // Validate members
            if (m_Controller == null)
                return Status.Error;

            if ((m_Controller.collisionFlags & collisionFlag) != 0) {
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