//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Performs math operations on int values, stores the result in "Store".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Performs math operations on int values, stores the result in \"Store\"")]
    public class IntOperation : ActionNode {

    	/// <summary>
        /// The int values to perform the operation.
        /// </summary>
        [VariableInfo(tooltip = "The int values to perform the operation")]
        public IntVar[] values;


        [Tooltip("The operation to perform")]
        public Operation operation;

        /// <summary>
        /// Stores the operation result.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the operation result")]
        public IntVar store;

        public override void Reset () {
            values = new IntVar[] {new ConcreteIntVar(), new ConcreteIntVar()};
            operation = Operation.Add;
            store = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (values.Length <= 1 || store.isNone)
                return Status.Error;

            int result = 0;

            // Do operation
            switch (operation) {
                case Operation.Add:
                    foreach (var v in values) result += v.Value;
                    break;
                case Operation.Subtract:
                    foreach (var v in values) result -= v.Value;
                    break;
                case Operation.Multiply:
                    result = 1;
                    foreach (var v in values) result *= v.Value;
                    break;
                case Operation.Divide:
                    result = values[0];
                    for (int i = 0; i < values.Length; i++) result /= values[i].Value;
                    break;
                case Operation.Max:
                    var parametersMax = new int[values.Length];
                    for (int i = 0; i < values.Length; i++) parametersMax[i] = values[i].Value;
                    result = Mathf.Max(parametersMax);
                    break;
                case Operation.Min:
                    var parametersMin = new int[values.Length];
                    for (int i = 0; i < values.Length; i++) parametersMin[i] = values[i].Value;
                    result = Mathf.Min(parametersMin);
                    break;
            }

            store.Value = result;

            return Status.Success;
        }
    }
}