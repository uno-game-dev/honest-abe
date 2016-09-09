//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Forces a rigidbody to wake up.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Forces a rigidbody to wake up")]
    public class WakeUp : ActionNode {

    	/// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the rigidbody
            var rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (rigidbody == null)
                return Status.Error;

            rigidbody.WakeUp();
            return Status.Success;
        }
    }
}