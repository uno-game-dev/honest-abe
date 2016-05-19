//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Forces all rigidbodies in the scene to wake up.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Forces all rigidbodies in the scene to wake up")]
    public class WakeUpAll2D : ActionNode {

        public override Status Update () {
            // Get rigidbodies
            var rigidbodies = Object.FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
            
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
#endif