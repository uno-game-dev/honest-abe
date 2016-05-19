//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" direction.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets the \"Game Object\" direction")]
    public class SetDirection : ActionNode {

        /// <summary>
        /// The game object to set the direction.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the direction")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new direction.
        /// </summary>
        [VariableInfo(tooltip = "The new direction")]
        public Vector3Var newDirection;

        /// <summary>
        /// The minimum sqrMagnitude of the 'Target Direction' to apply the rotation.
        /// </summary>
        [VariableInfo(tooltip = "The minimum sqrMagnitude of the \'Target Direction\' to apply the rotation")]
        public FloatVar minSqrMagnitude;

        /// <summary>
        /// The up direction.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Up (green axis)", tooltip = "The up direction")]
        public Vector3Var upDirection;

        /// <summary>
        /// Should ignore the y axis?
        /// </summary>
        [VariableInfo(tooltip = "Should ignore the y axis?")]
        public BoolVar ignoreYAxis;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newDirection = new ConcreteVector3Var();
            upDirection = new ConcreteVector3Var();
            minSqrMagnitude = 0.1f;
            ignoreYAxis = true;
        }

        public override Status Update () {
            // Validate members
            if  (gameObject.transform == null || newDirection.isNone || ignoreYAxis.isNone)
                return Status.Error;

            // Get the direction
            Vector3 direction = newDirection.Value;

            // Ignore y axis?
            if (ignoreYAxis.Value)
                direction.y = 0f;

            // The direction sqrMagnitude is greater than the minSqrMagnitude?
            if (direction.sqrMagnitude > minSqrMagnitude.Value) {
                // Set direction
                gameObject.transform.rotation = Quaternion.LookRotation(direction, !upDirection.isNone ? upDirection.Value : Vector3.up);
            }

            return Status.Success;
        }
    }
}