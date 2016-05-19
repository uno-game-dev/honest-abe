//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Inverts the "variable" direction.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Inverts \"X\", \"Y\" or \"Z\" components of \"variable\"")]
    public class Vector3InvertXYZ : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to invert direction")]
        public Vector3Var variable;

        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Invert \"Variable.x\"?")]
        public BoolVar x;

        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Invert \"Variable.y\"?")]
        public BoolVar y;

        [VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Invert \"Variable.z\"?")]
        public BoolVar z;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            x = new ConcreteBoolVar();
            y = new ConcreteBoolVar();
            z = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            var v3 = variable.Value;

            if (!x.isNone && x.Value) v3.x = -1 * v3.x;
            if (!y.isNone && y.Value) v3.y = -1 * v3.y;
            if (!z.isNone && z.Value) v3.z = -1 * v3.z;

            variable.Value = v3;

            return Status.Success;
        }

    }
}