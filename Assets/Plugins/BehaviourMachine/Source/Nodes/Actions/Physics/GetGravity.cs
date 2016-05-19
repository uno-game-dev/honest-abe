//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the gravity value.
    /// </summary>
    [NodeInfo ( category = "Action/Physics/",
                icon = "Physics",
                description = "Gets the gravity value",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics-gravity.html")]
    public class GetGravity : ActionNode {

        /// <summary>
        /// Stores the gravity value.
        /// </summary>
        [VariableInfo(tooltip = "Stores the gravity value")]
    	public Vector3Var storeGravity;

        public override void Reset () {
            storeGravity = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (storeGravity.isNone)
                return Status.Error;

            // Store gravity
            storeGravity.Value = Physics.gravity;
            return Status.Success;
        }
    }
}
