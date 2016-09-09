//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the owner ignore time scale.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "UnityEditor.AnimationWindow",
                description = "Sets the owner ignore time scale")]
	public class SetTreeIgnoreTimeScale : ActionNode {

		/// <summary>
        /// The new ignore time scale value. If Toggle is selected the value of ignore time scale is flipped.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The new ignore time scale value. If Toggle is selected the value of ignore time scale is flipped")]
        public BoolVar newValue;

        public override void Reset () {
            newValue = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Set ignore time scale
            this.owner.ignoreTimeScale = newValue.isNone ? !this.owner.ignoreTimeScale : newValue.Value;
            return Status.Success;
        }
	}
}