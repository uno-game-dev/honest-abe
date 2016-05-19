//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a color value.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a color value")]
    public class ColorRandom : ActionNode {

        /// <summary>
        /// Store the random selected color.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected color")]
        public ColorVar storeColor;

        public override void Reset () {
            storeColor = new ConcreteColorVar();
        }

        public override Status Update () {
            // Validate members
            if (storeColor.isNone)
                return Status.Error;

            // Randomly selects a color
            storeColor.Value = new Color (Random.value, Random.value, Random.value, Random.value);
            return Status.Success;
        }

    }
}