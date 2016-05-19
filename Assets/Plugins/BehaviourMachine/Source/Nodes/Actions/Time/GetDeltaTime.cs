//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the delta time.
    /// </summary>
    [NodeInfo(  category = "Action/Time/",
                icon = "UnityEditor.AnimationWindow",
                description = "Gets the delta time")]
    public class GetDeltaTime : ActionNode {

        /// <summary>
        /// Store the delta time.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the delta time")]
        public FloatVar storeDeltaTime;

        public override void Reset () {
            storeDeltaTime = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (storeDeltaTime.isNone)
                return Status.Error;

            storeDeltaTime.Value = this.owner.deltaTime;
            
            return Status.Success;
        }
    }
}