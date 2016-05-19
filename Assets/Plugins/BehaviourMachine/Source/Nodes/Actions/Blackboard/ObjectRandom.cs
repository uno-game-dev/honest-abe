//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of objects.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a object in a set of objects")]
    public class ObjectRandom : ActionNode {

        /// <summary>
        /// The possible object values.
        /// </summary>
        [VariableInfo(tooltip = "The possible object values")]
        public ObjectVar[] objects;

        /// <summary>
        /// Store the random selected object.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected object")]
        public ObjectVar storeObject;

        public override void Reset () {
            objects = new ObjectVar[0];
            storeObject = new ConcreteObjectVar();
        }

        public override Status Update () {
            // Validate members
            if (objects.Length == 0 || storeObject.isNone)
                return Status.Error;

            // Randomly selects an object
            storeObject.Value = objects[Random.Range(0, objects.Length)].Value;
            return Status.Success;
        }

    }
}