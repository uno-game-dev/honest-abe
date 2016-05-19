//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Stores the value of the virtual axis identified by "Axis Name" in a component of "Store Axis".
    /// <summary>
    [NodeInfo(  category = "Action/Input/",
                icon = "Axis",
                description = "Stores the value of the virtual axis identified by \"Axis Name\" in a component of \"Store Axis\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input.GetAxis.html")]
    public class GetAxis2 : ActionNode {

        /// <summary>
        /// The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Horizontal", tooltip = "The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input")]
        public StringVar axisName;

        /// <summary>
        /// The component of the "Store Axis" to store the input axis value.
        /// </summary>
        [Tooltip("The component of the \"Store Axis\" to store the input axis value")]
        public Vector3Component storeAxisComponent;

        /// <summary>
        /// The variable to store the axis.
        /// <summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to store the axis")]
        public Vector3Var storeAxis;

        /// <summary>
        /// An optional float to multiply the input before store in "Store Axis".
        /// <summary>
        [VariableInfo(tooltip = "An optional float to multiply the input before store in \"Store Axis\"")]
        public FloatVar multiplier;

        public override void Reset () {
            axisName = new ConcreteStringVar();
            storeAxisComponent = Vector3Component.x;
            storeAxis = new ConcreteVector3Var();
            multiplier = 1f;
        }

        public override Status Update () {
            // Validate members
            if (storeAxis.isNone || multiplier.isNone)
                return Status.Error;

            // Get axis
            float axis = Input.GetAxis(axisName.isNone ? "Horizontal" : axisName.Value);

            // Multiply
            axis *= multiplier.Value;

            // Store
            switch (storeAxisComponent) {
                case Vector3Component.x:
                    Vector3 oldStoreAxis = storeAxis.Value;
                    storeAxis.Value = new Vector3(axis, oldStoreAxis.y, oldStoreAxis.z);
                    break;
                case Vector3Component.y:
                    oldStoreAxis = storeAxis.Value;
                    storeAxis.Value = new Vector3(oldStoreAxis.x, axis, oldStoreAxis.z);
                    break;
                case Vector3Component.z:
                    oldStoreAxis = storeAxis.Value;
                    storeAxis.Value = new Vector3(oldStoreAxis.x, oldStoreAxis.y, axis);
                    break;
            }

            return Status.Success;
        }
    }
}