//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of a Rigidbody's useGravity property.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Sets the value of a Rigidbody\'s useGravity property",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody-useGravity.html")]
    public class SetUseGravity : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new value of the useGravity property. If Toggle, the value of useGravity will be flipped.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The new value of the useGravity property. If Toggle, the value of useGravity will be flipped")]
        public BoolVar newUseGravity;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newUseGravity = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the rigidbody
            var rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (rigidbody == null)
                return Status.Error;

            // Set the useGravity
            rigidbody.useGravity = newUseGravity.isNone ? !rigidbody.useGravity : newUseGravity.Value;

            return Status.Success;
        }
    }
}