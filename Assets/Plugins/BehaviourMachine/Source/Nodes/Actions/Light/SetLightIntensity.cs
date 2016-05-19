//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the intensity of a light.
    /// </summary>
    [NodeInfo ( category = "Action/Light/",
                icon = "Light",
                description = "Sets the intensity of a light",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Light-intensity.html")]
    public class SetLightIntensity : ActionNode {
        /// <summary>
        /// The game object that has a light in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a light in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new intensity of the light.
        /// </summary>
        [VariableInfo(tooltip = "The new intensity of the light")]
        public FloatVar newIntensity;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newIntensity = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the light
            Light light = gameObject.Value != null? gameObject.Value.GetComponent<Light>() : null;

            // Validate members
            if (light == null || newIntensity.isNone)
                return Status.Error;

            // Set light intensity
            light.intensity = newIntensity.Value;
            return Status.Success;
        }
    }
}