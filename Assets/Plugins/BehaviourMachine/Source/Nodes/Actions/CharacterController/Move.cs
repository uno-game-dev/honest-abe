//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// A more complex move function taking absolute movement deltas (requires a Character Controller).
    /// It is recommended that you make only one call to Move per frame.
    /// </summary>
    [NodeInfo(  category = "Action/CharacterController/",
                icon = "CharacterController",
                description = "A more complex move function taking absolute movement deltas (requires a Character Controller). It is recommended that you make only one call to Move per frame",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/CharacterController.Move.html")]
    public class Move : ActionNode {

        /// <summary>
        /// A game object that has a characterController.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has a characterController")]
        public GameObjectVar gameObject;

        public Space space = Space.World;

        /// <summary>
        /// The direction to move towards.
        /// </summary>
        [VariableInfo(tooltip = "The direction to move towards")]
        public Vector3Var direction;

        /// <summary>
        /// The speed to move.
        /// </summary>
        [VariableInfo(tooltip = "The speed to move")]
        public FloatVar speed;

        /// <summary>
        /// Use gravity during movement?.
        /// </summary>
        [VariableInfo(tooltip = "Use gravity during movement?")]
        public BoolVar useGravity;

        /// <summary>
        /// If true the character controller will jump. The value is triggered to false in the beginning of the jump.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Jump", tooltip = "It true the character controller will jump. The value is triggered to false in the beginning of the jump")]
        public BoolVar jump;

        /// <summary>
        /// Jump speed when the character controller jumps.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Jump", tooltip = "Jump speed when the character controller jumps")]
        public FloatVar jumpSpeed;

        [System.NonSerialized]
        CharacterController m_Controller = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            space = Space.World;
            direction = new ConcreteVector3Var();
            speed = 1f;
            useGravity = true;
            jump = new ConcreteBoolVar();
            jumpSpeed = 8f;
        }

        public override Status Update () {
            // Get the controller
            if (m_Controller == null || m_Controller.gameObject != gameObject.Value)
                m_Controller = gameObject.Value != null ? gameObject.Value.GetComponent<CharacterController>() : null;

            // Validate members
            if (m_Controller == null || direction.isNone || speed.isNone || useGravity.isNone)
                return Status.Error;

            // Get velocity direction
            var velocityDirection = (space == Space.Self) ? m_Controller.transform.TransformDirection(direction.Value) : direction.Value;
            velocityDirection *= speed.Value;

            // Apply gravity?
            if (useGravity.Value) {
                if (m_Controller.isGrounded)
                    velocityDirection.y -= 0.9f;   //Apply the magic number
                else {
                    velocityDirection += Physics.gravity * owner.deltaTime;
                    velocityDirection.y += m_Controller.velocity.y;
                }

                // Should Jump?
                if (jump.Value) {
                    // Add vertical speed.
                    velocityDirection.y += jumpSpeed.Value;

                    // Make sure doesn't jump again
                    jump.Value = false;
                }
            }

            // Move
            m_Controller.Move(velocityDirection * owner.deltaTime);

            return Status.Success;
        }
    }
}