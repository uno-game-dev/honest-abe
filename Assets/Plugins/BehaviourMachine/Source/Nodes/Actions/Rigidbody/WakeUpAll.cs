//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Forces all rigidbodies in the scene to wake up.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Forces all rigidbodies in the scene to wake up")]
    public class WakeUpAll : ActionNode {

        public override Status Update () {
            // Get rigidbodies
            var rigidbodies = Object.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
            
            // Validate members
            if (rigidbodies == null)
                return Status.Error;

            // Wake up all rigidbodies
            for (int i = 0; i < rigidbodies.Length; i++)
                rigidbodies[i].WakeUp();

            return Status.Success;
        }
    }
}