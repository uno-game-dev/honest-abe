//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Requires a rigidbody 2d and can only be used in the FixeUpdate function.
    /// </summary>
    [NodeInfo(  category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Requires a rigidbody 2d and can only be used in the FixeUpdate function")]
    public class Rigidbody2DController : ActionNode, IFixedUpdateNode {

        /// <summary>
        /// The game object to move.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to move")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The input to move the rigidbody2D; usually the GetAxis action with Horizontal axis is used to get the input.
        /// </summary>
        [VariableInfo(tooltip = "The input to move the rigidbody2D; usually the GetAxis action with Horizontal axis is used to get the input")]
        public FloatVar input;

        /// <summary>
        /// Amount of force added to move the rigidbody2D. Hint: try 200.
        /// </summary>
        [VariableInfo(tooltip = "Amount of force added to move the rigidbody2D. Hint: try 200")]
        public FloatVar moveForce;

        /// <summary>
        /// The fastest the rigidbody2D can travel in the x axis: Hint: try 2.
        /// </summary>
        [VariableInfo(tooltip = "The fastest the rigidbody2D can travel in the x axis: Hint: try 2")]
        public FloatVar maxSpeed;

        /// <summary>
        /// It True the rigidbody2D will jump. The value is triggered to false in the beginning of the jump.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Jump", tooltip = "It True the rigidbody2D will jump. The value is triggered to False in the beginning of the jump")]
        public BoolVar jump;

        /// <summary>
        /// Amount of force added when the rigidbody2D jumps.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Jump", tooltip = "Amount of force added when the rigidbody2D jumps")]
        public FloatVar jumpForce;

        /// <summary>
        /// Stores True if the rigidbody2D is facing left; stores False if the rigidbody2D is facing right. If the rigidbody2D starts facing left you should init the value of "Store Facing Left".
        /// </summary>
        [VariableInfo(canBeConstant = false, requiredField = false, nullLabel = "Don't Store", tooltip = "Stores True if the rigidbody2D is facing left; stores False if the rigidbody2D is facing right. If the rigidbody2D starts facing left you should init the value of \"Store Facing Left\"")]
        public BoolVar storeFacingLeft;

        /// <summary>
        /// If True the transform.scale.x will be positive when the rigidbody2D is facing right and negative when the rigidbody2D is facing left.
        /// </summary>
        [Tooltip("If True the transform.scale.x will be positive when the rigidbody2D is facing right and negative when the rigidbody2D is facing left")]
        public bool flip = true;

        [System.NonSerialized]
        Rigidbody2D m_Rigidbody2D = null;

    	public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            input = new ConcreteFloatVar();
            moveForce = new ConcreteFloatVar();
            maxSpeed = new ConcreteFloatVar();
            jump = new ConcreteBoolVar();
            jumpForce = 500f;
            flip = true;
        }

        public override Status Update () {
            // Get the rigidbody2D
            if (m_Rigidbody2D == null || m_Rigidbody2D.gameObject != gameObject.Value)
                m_Rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (m_Rigidbody2D == null || input.isNone)
                return Status.Error;

            // Get the input value
            float inputValue = input.Value;

            // Apply force
            if(inputValue * m_Rigidbody2D.velocity.x < maxSpeed)
                m_Rigidbody2D.AddForce(Vector2.right * inputValue * moveForce.Value);

            // Check max velocity
            if(Mathf.Abs(m_Rigidbody2D.velocity.x) > maxSpeed.Value)
                m_Rigidbody2D.velocity = new Vector2(Mathf.Sign(m_Rigidbody2D.velocity.x) * maxSpeed.Value, m_Rigidbody2D.velocity.y);

            // Should Jump?
            if (jump.Value) {
                // Add a vertical force to the player.
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce.Value));

                // Make sure doesn't jump again
                jump.Value = false;
            }

            // Is the input moving the player right and the player is facing left?
            if(inputValue > 0f && storeFacingLeft.Value) {
                storeFacingLeft.Value = false;

                // Need to flip transform.position.x?
                if (flip)
                    FlipX();
            }
            // Is the input moving the player left and the player is facing right?
            else if(inputValue < 0f && !storeFacingLeft.Value) {
                storeFacingLeft.Value = true;

                // Need to flip transform.position.x?
                if (flip)
                    FlipX();
            }

            return Status.Success;
        }

        void FlipX () {
            // Get the transform
            var transform = m_Rigidbody2D.transform;

            // Multiply the transform's x local scale by -1
            Vector3 theScale = transform.localScale;
            theScale.x *= -1f;
            transform.localScale = theScale;
        }
    }
}
#endif