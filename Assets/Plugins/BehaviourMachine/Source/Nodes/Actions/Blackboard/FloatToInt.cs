//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a float variable to an int.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a float variable to an int")]
    public class FloatToInt : ActionNode {

        public enum ConvertMethod {
            Round,
            Floor,
            Ceil
        }

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(tooltip = "The variable to be converted")]
        public FloatVar floatVariable;

        /// <summary>
        /// The method to convert the float.
        /// </summary>
        [Tooltip("The method to convert the float")]
        public FloatToInt.ConvertMethod convertMethod;

        /// <summary>
        /// Stores the int value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the int value")]
        public IntVar storeInt;


        public override void Reset () {
            floatVariable = 0f;
            convertMethod = FloatToInt.ConvertMethod.Round;
            storeInt = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (floatVariable.isNone || storeInt.isNone)
                return Status.Error;

            // Selects the method to convert the float
            switch (convertMethod) {
                case FloatToInt.ConvertMethod.Round:
                    storeInt.Value = Mathf.RoundToInt(floatVariable.Value);
                    break;
                case FloatToInt.ConvertMethod.Floor:
                    storeInt.Value = Mathf.FloorToInt(floatVariable.Value);
                    break;
                case FloatToInt.ConvertMethod.Ceil:
                    storeInt.Value = Mathf.CeilToInt(floatVariable.Value);
                    break;
            }

            return Status.Success;
        }

    }
}