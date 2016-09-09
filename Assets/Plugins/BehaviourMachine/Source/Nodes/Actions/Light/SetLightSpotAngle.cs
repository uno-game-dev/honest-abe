//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the spot angle of a light.
    /// </summary>
    [NodeInfo ( category = "Action/Light/",
                icon = "Light",
                description = "Sets the spot angle of a light",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Light-spotAngle.html")]
    public class SetLightSpotAngle : ActionNode {
        /// <summary>
        /// The game object that has a light in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a light in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The spot angle of the light.
        /// </summary>
        [VariableInfo(tooltip = "The spot angle of the light")]
        public FloatVar newSpotAngle;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newSpotAngle = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the light
            Light light = gameObject.Value != null? gameObject.Value.GetComponent<Light>() : null;

            // Validate members
            if (light == null || newSpotAngle.isNone)
                return Status.Error;

            // Set light spot angle
            light.spotAngle = newSpotAngle.Value;
            return Status.Success;
        }
    }
}