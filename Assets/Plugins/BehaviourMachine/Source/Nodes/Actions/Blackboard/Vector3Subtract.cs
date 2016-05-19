//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Subracts "Subtract" value from "Variable".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Subracts \"Subtract\" value from \"Variable\"")]
    public class Vector3Subtract : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public Vector3Var variable;

        /// <summary>
        /// The value to subtract from "Variable".
        /// </summary>
        [VariableInfo(tooltip = "The value to subtract from \"Variable\"")]
        public Vector3Var subtract;

        /// <summary>
        /// If true the operation will be applied every second; otherwise the operation will be applied instantaneously
        /// </summary>
        [Tooltip("If true the operation will be applied every second; otherwise the operation will be applied instantaneously")]
        public bool perSecond = false;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            subtract = new ConcreteVector3Var();
            perSecond = false;
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || subtract.isNone)
                return Status.Error;

            if (perSecond)
                variable.Value -= subtract.Value * owner.deltaTime;
            else
                variable.Value -= subtract.Value;

            return Status.Success;
        }

    }
}