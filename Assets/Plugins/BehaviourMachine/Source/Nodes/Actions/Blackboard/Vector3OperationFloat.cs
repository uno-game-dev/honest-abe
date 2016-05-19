//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Performs math operation on "A" and "B", stores the result in the float variable "Store".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Performs math operation on \"A\" and \"B\", stores the result in the float variable \"Store\"")]
    public class Vector3OperationFloat : ActionNode {

        public enum V3OperationFloat {
            Angle,
            Distance,
            Dot
        }

        /// <summary>
        /// The first Vector3 of the operation.
        /// </summary>
        [VariableInfo(tooltip = "The first Vector3 of the operation")]
        public Vector3Var a;

        /// <summary>
        /// The operation to perform.
        /// </summary>
        [Tooltip("The operation to perform")]
        public V3OperationFloat operation;

        /// <summary>
        /// The second Vector3 of the operation.
        /// </summary>
        [VariableInfo(tooltip = "The second Vector3 of the operation")]
        public Vector3Var b;

        /// <summary>
        /// Stores the operation result ("Store" = "A" "Operation" "B").
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the operation result (\"Store\" = \"A\" \"Operation\" \"B\")")]
        public FloatVar store;

        public override void Reset () {
            a = new ConcreteVector3Var();
            b = new ConcreteVector3Var();
            store = new ConcreteFloatVar();
            operation = V3OperationFloat.Angle;
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone || store.isNone)
                return Status.Error;

            switch (operation) {
                case V3OperationFloat.Angle:
                    store.Value = Vector3.Angle(a.Value, b.Value);
                    break;
                case V3OperationFloat.Distance:
                    store.Value = Vector3.Distance(a.Value, b.Value);
                    break;
                case V3OperationFloat.Dot:
                    store.Value = Vector3.Dot(a.Value, b.Value);
                    break;
            }

            return Status.Success;
        }

    }
}