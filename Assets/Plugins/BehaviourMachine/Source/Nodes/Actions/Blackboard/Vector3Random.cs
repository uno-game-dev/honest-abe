//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of Vector3s.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a value in a set of Vector3s")]
    public class Vector3Random : ActionNode {

        /// <summary>
        /// The possible Vector3 values.
        /// </summary>
        [VariableInfo(tooltip = "The possible Vector3 values")]
        public Vector3Var[] vector3s;

        /// <summary>
        /// Store the random selected Vector3.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected Vector3")]
        public Vector3Var storeVector3;

        public override void Reset () {
            vector3s = new Vector3Var[0];
            storeVector3 = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (vector3s.Length == 0 || storeVector3.isNone)
                return Status.Error;

            // Randomly selects a Vector3
            storeVector3.Value = vector3s[Random.Range(0, vector3s.Length)].Value;
            return Status.Success;
        }

    }
}