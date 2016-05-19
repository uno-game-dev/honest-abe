//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the "Variable" value.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Changes the \"Variable\" value")]
    public class GameObjectSet : ActionNode {

        /// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to change value")]
        public GameObjectVar variable;

        /// <summary>
        /// The new variable value.
        /// </summary>
        [VariableInfo(tooltip = "The new variable value")]
        public GameObjectVar newValue;

        public override void Reset () {
            variable = new ConcreteGameObjectVar(this.self);
            newValue = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || newValue.isNone)
                return Status.Error;

            variable.Value = newValue.Value;

            return Status.Success;
        }
    }
}