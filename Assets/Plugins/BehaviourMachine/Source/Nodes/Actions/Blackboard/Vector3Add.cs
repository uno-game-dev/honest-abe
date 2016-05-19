//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds "Add" value to "Variable".
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Adds \"Add\" value to \"Variable\"")]
    public class Vector3Add : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public Vector3Var variable;

        /// <summary>
        /// The value to add to "Variable".
        /// </summary>
        [VariableInfo(tooltip = "The value to add to \"Variable\"")]
        public Vector3Var add;

        /// <summary>
        /// If true the operation will be applied every second; otherwise the operation will be applied instantaneously
        /// </summary>
        [Tooltip("If true the operation will be applied every second; otherwise the operation will be applied instantaneously")]
        public bool perSecond = false;

        public override void Reset () {
            variable = new ConcreteVector3Var();
            add = new ConcreteVector3Var();
            perSecond = false;
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || add.isNone)
                return Status.Error;

            if (perSecond)
                variable.Value += add.Value * owner.deltaTime;
            else
                variable.Value += add.Value;

            return Status.Success;
        }

    }
}