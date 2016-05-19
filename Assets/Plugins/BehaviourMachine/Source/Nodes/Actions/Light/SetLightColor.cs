//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the color of a light.
    /// </summary>
    [NodeInfo ( category = "Action/Light/",
                icon = "Light",
                description = "Sets the color of a light",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Light-color.html")]
    public class SetLightColor : ActionNode {
        /// <summary>
        /// The game object that has a light in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a light in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new color of the light.
        /// </summary>
        [VariableInfo(tooltip = "The new color of the light")]
        public ColorVar newColor;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newColor = new ConcreteColorVar();
        }

        public override Status Update () {
            // Get the light
            Light light = gameObject.Value != null? gameObject.Value.GetComponent<Light>() : null;

            // Validate members
            if (light == null || newColor.isNone)
                return Status.Error;

            // Set light color
            light.color = newColor.Value;
            return Status.Success;
        }
    }
}