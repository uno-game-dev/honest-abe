//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds "X" to "Variable.x", "Y" to "Variable.y" and "Z" to "Variable.z".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Adds \"X\" to \"Variable.x\", \"Y\" to \"Variable.y\" and \"Z\" to \"Variable.z\"")]
    public class Vector3AddXYZ : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public Vector3Var variable;

        /// <summary>
        /// Adds "X" to "Variable.x".
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Adds \"X\" to \"Variable.x\"")]
        public FloatVar x;

        /// <summary>
        /// Adds "Y" to "Variable.y".
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Adds \"Y\" to \"Variable.y\"")]
        public FloatVar y;

        /// <summary>
        /// Adds "Z" to "Variable.z".
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Adds \"Z\" to \"Variable.z\"")]
        public FloatVar z;

        /// <summary>
        /// If true the operation will be applied every second; otherwise the operation will be applied instantaneously
        /// </summary>
        [Tooltip("If true the operation will be applied every second; otherwise the operation will be applied instantaneously")]
        public bool perSecond = false;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            perSecond = false;
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone)
                return Status.Error;

            Vector3 newValue = variable.Value;

            if (perSecond) {
                float deltaTime = owner.deltaTime;
                if (!x.isNone) newValue.x += x.Value * deltaTime;
                if (!y.isNone) newValue.y += y.Value * deltaTime;
                if (!z.isNone) newValue.z += z.Value * deltaTime;
            }
            else {
                if (!x.isNone) newValue.x += x.Value;
                if (!y.isNone) newValue.y += y.Value;
                if (!z.isNone) newValue.z += z.Value;
            }

            variable.Value = newValue;

            return Status.Success;
        }

    }
}