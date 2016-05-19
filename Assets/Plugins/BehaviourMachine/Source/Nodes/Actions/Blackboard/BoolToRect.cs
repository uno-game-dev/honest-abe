//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a Rect.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a Rect")]
    public class BoolToRect : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The Rect value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(tooltip = "The Rect value to be stored if the \"Bool Variable\" is True")]
        public RectVar trueValue;

        /// <summary>
        /// The Rect value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(tooltip = "The Rect value to be stored if the \"Bool Variable\" is False")]
        public RectVar falseValue;

        /// <summary>
        /// Stores the Rect value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the Rect value")]
        public RectVar storeRect;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = new Rect(1,1,1,1);
            falseValue = new Rect(0,0,0,0);
            storeRect = new ConcreteRectVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || trueValue.isNone || falseValue.isNone || storeRect.isNone)
                return Status.Error;

            storeRect.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}