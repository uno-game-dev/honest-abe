//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Performs math operation on "A" and "B", stores the result in "Store".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Performs math operation on \"A\" and \"B\", stores the result in \"Store\"")]
    public class Vector3Operation : ActionNode {

        public enum V3Operation {
            Cross,
            Max,
            Min,
            Project,
            Reflect,
            Scale
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
        public V3Operation operation;

        /// <summary>
        /// The second Vector3 of the operation.
        /// </summary>
        [VariableInfo(tooltip = "The second Vector3 of the operation")]
        public Vector3Var b;

        /// <summary>
        /// Stores the operation result ("Store" = "A" "Operation" "B").
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the operation result (\"Store\" = \"A\" \"Operation\" \"B\")")]
        public Vector3Var store;

        public override void Reset () {
            a = new ConcreteVector3Var();
            b = new ConcreteVector3Var();
            store = new ConcreteVector3Var();
            operation = V3Operation.Cross;
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone || store.isNone)
                return Status.Error;

            switch (operation) {
                case V3Operation.Cross:
                    store.Value = Vector3.Cross(a.Value, b.Value);
                    break;
                case V3Operation.Max:
                    store.Value = Vector3.Max(a.Value, b.Value);
                    break;
                case V3Operation.Min:
                    store.Value = Vector3.Min(a.Value, b.Value);
                    break;
                case V3Operation.Project:
                    store.Value = Vector3.Project(a.Value, b.Value);
                    break;
                case V3Operation.Reflect:
                    store.Value = Vector3.Reflect(a.Value, b.Value);
                    break;
                case V3Operation.Scale:
                    store.Value = Vector3.Scale(a.Value, b.Value);
                    break;
            }

            return Status.Success;
        }

    }
}