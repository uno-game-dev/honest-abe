//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The "Game Object" will smooth follows the "Target".
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "The \"Game Object\" will smooth follows the \"Target\"")]
    public class SmoothFollow : ActionNode {

    	/// <summary>
        /// The game object to be moved and/or rotated.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to be moved and/or rotated")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The target game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The target game object")]
        public GameObjectVar target;

        /// <summary>
        /// The movement speed.
        /// </summary>
        [VariableInfo (tooltip = "The movement speed")]
        public FloatVar moveSpeed;

        /// <summary>
        /// The turn speed.
        /// </summary>
        [VariableInfo (tooltip = "The turn speed")]
        public FloatVar turnSpeed;

        public override void Reset () {
            gameObject = this.self;
            moveSpeed = 3f;
            turnSpeed = 3f;
        }

        public override Status Update () {
            // Validate members
            if (moveSpeed.isNone || turnSpeed.isNone)
                return Status.Error;

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position, moveSpeed.Value * owner.deltaTime);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target.transform.rotation, turnSpeed.Value * owner.deltaTime);

            return Status.Success;
        }
    }
}