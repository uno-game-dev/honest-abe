//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Stores "Variable.x", "Variable.y" and "Variable.z" values in "Store X", "Store Y" and "Store Z".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Stores \"Variable.x\", \"Variable.y\" and \"Variable.z\" values in \"Store X\", \"Store Y\" and \"Store Z\"")]
    public class Vector3GetXYZ : ActionNode {

        /// <summary>
        /// The variable to get x, y and z values.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public Vector3Var variable;

        /// <summary>
        /// Stores variable.x.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Stores variable.x")]
        public FloatVar storeX;

        /// <summary>
        /// Stores variable.y.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Stores variable.y")]
        public FloatVar storeY;

        /// <summary>
        /// Stores variable.z.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Stores variable.z")]
        public FloatVar storeZ;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            storeX = new ConcreteFloatVar();
            storeY = new ConcreteFloatVar();
            storeZ = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            // Get value
            var value = variable.Value;
            storeX.Value = value.x;
            storeY.Value = value.y;
            storeZ.Value = value.z;

            return Status.Success;
        }

    }
}