//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the current relative velocity of the character controller in the "Game Object". Only works if you are using Move or SimpleMove to move the character controller.
    /// </summary>
    [NodeInfo ( category = "Action/CharacterController/",
                icon = "CharacterController",
                description = "Gets the current relative velocity of the character controller in the \"Game Object\". Only works if you are using Move or SimpleMove to move the character controller",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/CharacterController-velocity.html")]
    public class GetCharacterControllerVelocity : ActionNode {

    	/// <summary>
        /// The game object that has a character controller in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a character controller in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the character controller velocity.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store character controller velocity")]
        public Vector3Var velocity;

        /// <summary>
        /// Store horizontal velocity (Vector3(velocity.x, 0,velocity.z).magnitude).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store horizontal velocity (Vector3(velocity.x, 0,velocity.z).magnitude)")]
        public FloatVar horizontalVelocity;

        /// <summary>
        /// Store vertical velocity (velocity.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", canBeConstant = false, tooltip = "Store vertical velocity (velocity.y)")]
        public FloatVar verticalVelocity;

        [System.NonSerialized]
        CharacterController m_Controller = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            velocity = new ConcreteVector3Var();
            horizontalVelocity = new ConcreteFloatVar();
            verticalVelocity = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the controller
            if (m_Controller == null || m_Controller.gameObject != gameObject.Value)
                m_Controller = gameObject.Value != null ? gameObject.Value.GetComponent<CharacterController>() : null;

            // Validate members
            if (m_Controller == null)
                return Status.Error;

            // Get velocity
            var vel = m_Controller.velocity;

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