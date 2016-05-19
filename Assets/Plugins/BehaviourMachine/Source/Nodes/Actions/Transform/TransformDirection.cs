//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Transforms direction from local space to world space.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Transforms direction from local space to world space")]
    public class TransformDirection : ActionNode {

        /// <summary>
        /// The target game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The target game object")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The local direction to transform.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction to transform")]
        public Vector3Var localDirection;

        /// <summary>
        /// The local direction in the x axis (overrides localDirection.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction in the x axis (overrides localDirection.x)")]
        public FloatVar localX;

        /// <summary>
        /// The local direction in the y axis (overrides localDirection.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction in the y axis (overrides localDirection.y)")]
        public FloatVar localY;

        /// <summary>
        /// The local direction in the z axis (overrides localDirection.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction in the z axis (overrides localDirection.z)")]
        public FloatVar localZ;

        /// <summary>
        /// The local direction to transform.
        /// </summary>
        [VariableInfo(tooltip = "The local direction to transform")]
        public Vector3Var storeWorldDirection;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            localDirection = new ConcreteVector3Var();
            localX = new ConcreteFloatVar();
            localY = new ConcreteFloatVar();
            localZ = new ConcreteFloatVar();
            storeWorldDirection = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members?
            if  (gameObject.transform == null || storeWorldDirection.isNone)
                return Status.Error;

            // Get the local direction
            Vector3 targetDirection = localDirection.Value;

            // Override values?
            if (!localX.isNone) targetDirection.x = localX.Value;
            if (!localY.isNone) targetDirection.y = localY.Value;
            if (!localZ.isNone) targetDirection.z = localZ.Value;

            // Calcaulate the transform direction
            storeWorldDirection.Value = gameObject.transform.TransformDirection(targetDirection);

            return Status.Success;
        }
    }
}