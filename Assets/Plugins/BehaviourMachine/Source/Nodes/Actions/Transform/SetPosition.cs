//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" position.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets the \"Game Object\" position")]
    public class SetPosition : ActionNode {

        /// <summary>
        /// The game object to set the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new position")]
        public Vector3Var newPosition;

        /// <summary>
        /// The new position in the x axis (overrides newPosition.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new position.x axis (overrides newPosition.x)")]
        public FloatVar newX;

        /// <summary>
        /// The new position in the y axis (overrides newPosition.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new position.y axis (overrides newPosition.y)")]
        public FloatVar newY;

        /// <summary>
        /// The new position in the z axis (overrides newPosition.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new position.z axis (overrides newPosition.z)")]
        public FloatVar newZ;

        /// <summary>
        /// Self, sets the position relative to the parent's position. World, sets the position in world space.
        /// </summary>
        [Tooltip("Self, sets the position relative to the parent's position. World, sets the position in world space")]
        public Space relativeTo = Space.Self;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newPosition = new ConcreteVector3Var();
            newX = new ConcreteFloatVar();
            newY = new ConcreteFloatVar();
            newZ = new ConcreteFloatVar();
            relativeTo = Space.World;
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.transform;

            // Validate members?
            if  (transform == null)
                return Status.Error;

            // Get the position
            Vector3 position;

            if (newPosition.isNone)
                position = relativeTo == Space.Self ? transform.localPosition : transform.position;
            else
                position = newPosition.Value;

            // Override values?
            if (!newX.isNone) position.x = newX.Value;
            if (!newY.isNone) position.y = newY.Value;
            if (!newZ.isNone) position.z = newZ.Value;

            // Local position?
            if (relativeTo == Space.Self)
                transform.localPosition = position;
            else
                transform.position = position;

            return Status.Success;
        }
    }
}