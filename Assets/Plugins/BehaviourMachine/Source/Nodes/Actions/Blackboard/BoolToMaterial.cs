//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a Material.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a Material")]
    public class BoolToMaterial : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The Material value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The Material value to be stored if the \"Bool Variable\" is True")]
        public MaterialVar trueValue;

        /// <summary>
        /// The Material value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The Material value to be stored if the \"Bool Variable\" is False")]
        public MaterialVar falseValue;

        /// <summary>
        /// Stores the Material value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the Material value")]
        public MaterialVar storeMaterial;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = (Material)null;
            falseValue = (Material)null;
            storeMaterial = new ConcreteMaterialVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeMaterial.isNone)
                return Status.Error;

            storeMaterial.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}