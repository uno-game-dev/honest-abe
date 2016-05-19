//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the color component values.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Changes the color component values")]
    public class ColorSetRGBA : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public ColorVar variable;

        /// <summary>
        /// The new red component value (0 - 1).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new red component value")]
        public FloatVar newR;

        /// <summary>
        /// The new green component value (0 - 1).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new green component value")]
        public FloatVar newG;

        /// <summary>
        /// The new blue component value (0 - 1).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new blue component value")]
        public FloatVar newB;

        /// <summary>
        /// The new alpha component value (0 - 1).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The new alpha component value")]
        public FloatVar newA;

        public override void Reset () {
            variable = new ConcreteColorVar();
            newR = new ConcreteFloatVar();
            newG = new ConcreteFloatVar();
            newB = new ConcreteFloatVar();
            newA = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            // Set color components
            Color color = variable.Value;
            if (!newR.isNone) color.r = newR.Value;
            if (!newG.isNone) color.g = newG.Value;
            if (!newB.isNone) color.b = newB.Value;
            if (!newA.isNone) color.a = newA.Value;
            variable.Value = color;

            return Status.Success;
        }

    }
}