//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Transforms a direction from world space to local space. The opposite of Transform.TransformDirection.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Transforms a direction from world space to local space. The opposite of Transform.TransformDirection")]
    public class InverseTransformDirection : ActionNode {

        /// <summary>
        /// The target game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The target game object")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The local direction to transform.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction to transform")]
        public Vector3Var worldDirection;

        /// <summary>
        /// The local direction in the x axis (overrides worldDirection.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction in the x axis (overrides worldDirection.x)")]
        public FloatVar localX;

        /// <summary>
        /// The local direction in the y axis (overrides worldDirection.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction in the y axis (overrides worldDirection.y)")]
        public FloatVar localY;

        /// <summary>
        /// The local direction in the z axis (overrides worldDirection.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The local direction in the z axis (overrides worldDirection.z)")]
        public FloatVar localZ;

        /// <summary>
        /// The local direction to transform.
        /// </summary>
        [VariableInfo(tooltip = "The local direction to transform")]
        public Vector3Var storeLocalDirection;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            worldDirection = new ConcreteVector3Var();
            localX = new ConcreteFloatVar();
            localY = new ConcreteFloatVar();
            localZ = new ConcreteFloatVar();
            storeLocalDirection = new ConcreteVector3Var();
        }

        public override Status Update () {

            // Validate members
            if  (gameObject.transform == null || storeLocalDirection.isNone)
                return Status.Error;

            // Get the local direction
            Vector3 targetDirection = worldDirection.Value;

            // Override values?
            if (!localX.isNone) targetDirection.x = localX.Value;
            if (!localY.isNone) targetDirection.y = localY.Value;
            if (!localZ.isNone) targetDirection.z = localZ.Value;

            // Calcaulate the transform direction
            storeLocalDirection.Value = gameObject.transform.InverseTransformDirection(targetDirection);

            return Status.Success;
        }
    }
}