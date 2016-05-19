//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Stores the value of the virtual axis identified by "Axis Name" in "Store Axis".
    /// <summary>
    [NodeInfo(  category = "Action/Input/",
                icon = "Axis",
                description = "Stores the value of the virtual axis identified by \"Axis Name\" in \"Store Axis\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input.GetAxis.html")]
    public class GetAxis : ActionNode {

        /// <summary>
        /// The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Horizontal" , tooltip = "The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input")]
        public StringVar axisName;

        /// <summary>
        /// The variable to store the axis.
        /// <summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to store the axis")]
        public FloatVar storeAxis;

        /// <summary>
        /// An optional float to multiply the input before store in "Store Axis".
        /// <summary>
        [VariableInfo(tooltip = "An optional float to multiply the input before store in \"Store Axis\"")]
        public FloatVar multiplier;

        public override void Reset () {
            axisName = new ConcreteStringVar();
            storeAxis = new ConcreteFloatVar();
            multiplier = 1f;
        }

        public override Status Update () {
            // Validate members
            if (storeAxis.isNone || multiplier.isNone)
                return Status.Error;

            // Get axis
            storeAxis.Value = Input.GetAxis(axisName.isNone ? "Horizontal" : axisName.Value);

            // Multiply
            storeAxis.Value *= multiplier.Value;

            return Status.Success;
        }
    }
}