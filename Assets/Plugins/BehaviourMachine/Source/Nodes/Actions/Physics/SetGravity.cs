//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the gravity value.
    /// </summary>
    [NodeInfo ( category = "Action/Physics/",
                icon = "Physics",
                description = "Changes the gravity value",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics-gravity.html")]
    public class SetGravity : ActionNode {

        /// <summary>
        /// The new gravity applied to all rigid bodies in the scene.
        /// </summary>
        [VariableInfo(tooltip = "The new gravity applied to all rigid bodies in the scene")]
    	public Vector3Var newGravity;

        public override void Reset () {
            newGravity = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (newGravity.isNone)
                return Status.Error;

            // Set gravity
            Physics.gravity = newGravity.Value;
            return Status.Success;
        }
    }
}
