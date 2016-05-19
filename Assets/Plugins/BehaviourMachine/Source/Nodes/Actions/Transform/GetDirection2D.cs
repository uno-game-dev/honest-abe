//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the "Game Object" direction in x, y axis. Transforms the "Local Direction" in global direction and stores in "Store Direction.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Gets the \"Game Object\" direction in x, y axis. Transforms the \"Local Direction\" in global direction and stores in \"Store Direction\"")]
    public class GetDirection2D : ActionNode {

        /// <summary>
        /// The game object to get the direction.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the direction")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The local direction, used to get the global direction.
        /// </summary>
        [VariableInfo (tooltip = "The local direction, used to get the global direction")]
        public Vector3Var localDirection;

        /// <summary>
        /// Multiply the direction.x by the sign of the transform.localScale.x of the direction?
        /// </summary>
        [Tooltip("Multiply the direction.x by the sign of the transform.localScale.x of the direction?")]
        public bool useScaleXSign = true;

        /// <summary>
        /// Store the direction in global space.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the direction in global space")]
        public Vector3Var storeDirection;

        /// <summary>
        /// Store the direction.x.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the direction.x")]
        public FloatVar storeX;

        /// <summary>
        /// Store the direction.y.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the direction.y")]
        public FloatVar storeY;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            localDirection = new ConcreteVector3Var();
            storeDirection = new ConcreteVector3Var();
            storeX = new ConcreteFloatVar();
            storeY = new ConcreteFloatVar();
            useScaleXSign = true;
        }

        public override Status Update () {
            // Validate members
            if  (gameObject.Value == null || localDirection.isNone)
                return Status.Error;

            // Get the direction
            var direction = gameObject.Value.transform.TransformDirection(localDirection.Value);

            // Use scale.x X?
            if (useScaleXSign)
                direction.x *= Mathf.Sign(gameObject.Value.transform.localScale.x);

            // Store direction
            storeDirection.Value = direction;
            storeX.Value = direction.x;
            storeY.Value = direction.y;

            return Status.Success;
        }
    }
}