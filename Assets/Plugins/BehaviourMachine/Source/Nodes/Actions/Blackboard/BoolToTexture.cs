//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a Texture.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a Texture")]
    public class BoolToTexture : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The Texture value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The Texture value to be stored if the \"Bool Variable\" is True")]
        public TextureVar trueValue;

        /// <summary>
        /// The Texture value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The Texture value to be stored if the \"Bool Variable\" is False")]
        public TextureVar falseValue;

        /// <summary>
        /// Stores the Texture value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the Texture value")]
        public TextureVar storeTexture;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = (Texture)null;
            falseValue = (Texture)null;
            storeTexture = new ConcreteTextureVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeTexture.isNone)
                return Status.Error;

            storeTexture.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}