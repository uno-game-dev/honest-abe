//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Flips the transform.x scale based on a float or boolean value.
    /// </summary>
    [NodeInfo(  category = "Action/Sprite/",
                icon = "SpriteRenderer",
                description = "Flips the transform.x scale based on a float or boolean value")]
    public class Flip2D : ActionNode {

    	/// <summary>
        /// The game object that has a SpriteRenderer in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a SpriteRenderer in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The direction of the movement.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The direction of the movement")]
        public FloatVar direction;

        public override void Reset () {
            gameObject = this.self;
            direction = new ConcreteFloatVar();

        }

        public override Status Update () {
            // Validate members
            if (direction.isNone)
                return Status.Error;

            if (direction.Value > 0f) {
                Vector3 localScale = gameObject.transform.localScale;
                // is facing left?
                if (localScale.x < 0f) {
                    localScale.x = -localScale.x;
                    gameObject.transform.localScale = localScale;
                }
            }
            else if (direction.Value < 0f) {
                Vector3 localScale = gameObject.transform.localScale;
                // is facing left?
                if (localScale.x > 0f) {
                    localScale.x = -localScale.x;
                    gameObject.transform.localScale = localScale;
                }
            }
            
            return Status.Success;
        }
    }
}