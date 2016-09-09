//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" scale.
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets the \"Game Object\" scale",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-localScale.html")]
    public class SetScale : ActionNode {

        /// <summary>
        /// The game object to set the scale.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the scale")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new scale.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use",tooltip = "The new scale")]
        public Vector3Var newScale;

        /// <summary>
        /// The new scale in the x axis (overrides New Scale.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new scale.x axis (overrides New Scale.x)")]
        public FloatVar newX;

        /// <summary>
        /// The new scale in the y axis (overrides New Scale.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new scale.y axis (overrides New Scale.y)")]
        public FloatVar newY;

        /// <summary>
        /// The new scale in the z axis (overrides New Scale.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new scale.z axis (overrides New Scale.z)")]
        public FloatVar newZ;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newScale = new ConcreteVector3Var();
            newX = new ConcreteFloatVar();
            newY = new ConcreteFloatVar();
            newZ = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (transform == null)
                return Status.Error;

            // Get the scale
            Vector3 scale = newScale.isNone ? transform.localScale : newScale.Value;

            // Override values?
            if (!newX.isNone) scale.x = newX.Value;
            if (!newY.isNone) scale.y = newY.Value;
            if (!newZ.isNone) scale.z = newZ.Value;

            // Set scale
            transform.localScale = scale;

            return Status.Success;
        }
    }
}