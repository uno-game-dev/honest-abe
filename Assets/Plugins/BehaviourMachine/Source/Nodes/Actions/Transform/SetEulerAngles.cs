//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" rotation using euler angles.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets the \"Game Object\" rotation using euler angles")]
    public class SetEulerAngles : ActionNode {

        /// <summary>
        /// The game object to set the rotation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the rotation")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new rotation in euler angles.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new rotation in euler angles")]
        public Vector3Var newEulerAngles;

        /// <summary>
        /// The new rotation in x axis (overrides eulerAngles.x).
        /// </summary>
        [VariableInfo(requiredField = false,nullLabel = "Don't Use", tooltip = "The new rotation in x axis (overrides eulerAngles.x)")]
        public FloatVar x;

        /// <summary>
        /// The new rotation in y axis (overrides eulerAngles.y).
        /// </summary>
        [VariableInfo(requiredField = false,nullLabel = "Don't Use", tooltip = "The new rotation in y axis (overrides eulerAngles.y)")]
        public FloatVar y;

        /// <summary>
        /// The new rotation in z axis (overrides eulerAngles.z).
        /// </summary>
        [VariableInfo(requiredField = false,nullLabel = "Don't Use", tooltip = "The new rotation in z axis (overrides eulerAngles.z)")]
        public FloatVar z;

        /// <summary>
        /// Self, sets the rotation relative to the parent's rotation. World, sets the rotation in world space.
        /// </summary>
        [Tooltip("Self, sets the rotation relative to the parent's rotation. World, sets the rotation in world space")]
        public Space relativeTo = Space.Self;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newEulerAngles = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members?
            if  (transform == null)
                return Status.Error;

            // Set euler angles
            if (relativeTo == Space.Self) {
                var eulerAngles = (!newEulerAngles.isNone) ? newEulerAngles.Value : transform.localEulerAngles;
                if (!x.isNone) eulerAngles.x = x.Value;
                if (!y.isNone) eulerAngles.y = y.Value;
                if (!z.isNone) eulerAngles.z = z.Value;
                transform.localEulerAngles = eulerAngles;
            }
            else {
                var eulerAngles = (!newEulerAngles.isNone) ? newEulerAngles.Value : transform.eulerAngles;
                if (!x.isNone) eulerAngles.x = x.Value;
                if (!y.isNone) eulerAngles.y = y.Value;
                if (!z.isNone) eulerAngles.z = z.Value;
                transform.eulerAngles = eulerAngles;
            }

            return Status.Success;
        }
    }
}