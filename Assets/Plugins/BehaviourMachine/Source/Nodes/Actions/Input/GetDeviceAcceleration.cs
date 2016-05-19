//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the last measured linear acceleration of a device in three-dimensional space.
    /// <summary>
    [NodeInfo(  category = "Action/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Gets the last measured linear acceleration of a device in three-dimensional space",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input-acceleration.html")]
    public class GetDeviceAcceleration : ActionNode {

        /// <summary>
        /// An optional float to multiply the acceleration before store in "Store Acceleration". If Normalize is selected the acceleration will only be normalized if the magnitude is more than 1.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Normalize", tooltip = "An optional float to multiply the acceleration before store in \"Store Acceleration\". If Normalize is selected the acceleration will only be normalized if the magnitude is more than 1")]
        public FloatVar multiplier;

        /// <summary>
        /// Variable to store the acceleration value.
        /// <summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Variable to store the acceleration value")]
        public Vector3Var storeAcceleration;

        /// <summary>
        /// Store the acceleration.x.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the acceleration.x")]
        public FloatVar storeX;

        /// <summary>
        /// Store the acceleration.y.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the acceleration.y")]
        public FloatVar storeY;

        /// <summary>
        /// Store the acceleration.z.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the acceleration.z")]
        public FloatVar storeZ;

        public override void Reset () {
            multiplier = 1f;
            storeAcceleration = new ConcreteVector3Var();
            storeX = new ConcreteFloatVar();
            storeY = new ConcreteFloatVar();
            storeZ = new ConcreteFloatVar();
        }

        public override Status Update () {

            // Get the acceleration
            Vector3 acceleration = Input.acceleration;

            // Multiply
            if (!multiplier.isNone)
                acceleration *= multiplier.Value;
            else if (acceleration.sqrMagnitude > 1f)
                acceleration.Normalize();

            // Store
            storeAcceleration.Value = acceleration;
            storeX.Value = acceleration.x;
            storeY.Value = acceleration.y;
            storeZ.Value = acceleration.z;

            return Status.Success;
        }
    }
}
