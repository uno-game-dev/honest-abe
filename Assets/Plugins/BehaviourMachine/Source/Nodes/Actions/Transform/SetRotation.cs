//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" rotation.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets the \"Game Object\" rotation")]
    public class SetRotation : ActionNode {

        /// <summary>
        /// The game object to set the rotation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the rotation")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new rotation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new rotation")]
        public QuaternionVar newRotation;

        /// <summary>
        /// The new rotation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new rotation")]
        public Vector3Var newEulerAngles;

        /// <summary>
        /// The new eulerAngles in the x axis (overrides 'New Rotation' or 'New Euler Angles' in the x axis).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new eulerAngles in the x axis (overrides 'New Rotation' or 'New Euler Angles' in the x axis)")]
        public FloatVar newX;

        /// <summary>
        /// The new eulerAngles in the y axis (overrides 'New Rotation' or 'New Euler Angles' in the y axis).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new eulerAngles in the y axis (overrides 'New Rotation' or 'New Euler Angles' in the y axis)")]
        public FloatVar newY;

        /// <summary>
        /// The new eulerAngles in the z axis (overrides 'New Rotation' or 'New Euler Angles' in the z axis).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new eulerAngles in the z axis (overrides 'New Rotation' or 'New Euler Angles' in the z axis)")]
        public FloatVar newZ;

        /// <summary>
        /// Self, sets the rotation relative to the parent's rotation. World, sets the rotation in world space.
        /// </summary>
        [Tooltip("Self, sets the rotation relative to the parent's rotation. World, sets the rotation in world space")]
        public Space relativeTo = Space.Self;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newRotation = new ConcreteQuaternionVar();
            newEulerAngles = new ConcreteVector3Var();
            newX = new ConcreteFloatVar();
            newY = new ConcreteFloatVar();
            newZ = new ConcreteFloatVar();
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (transform == null)
                return Status.Error;

            // Get the rotation
            Vector3 eulerAngles;

            if (!newRotation.isNone)
                eulerAngles = newRotation.Value.eulerAngles;
            else if (!newEulerAngles.isNone)
                eulerAngles = newEulerAngles.Value;
            else
                eulerAngles = relativeTo == Space.Self ? transform.localEulerAngles : transform.eulerAngles;

            // Override axis?
            if (!newX.isNone) eulerAngles.x = newX.Value;
            if (!newY.isNone) eulerAngles.y = newY.Value;
            if (!newZ.isNone) eulerAngles.z = newZ.Value;

            // Local rotation?
            if (relativeTo == Space.Self)
                transform.localEulerAngles = eulerAngles;
            else
                transform.eulerAngles = eulerAngles;

            return Status.Success;
        }
    }
}