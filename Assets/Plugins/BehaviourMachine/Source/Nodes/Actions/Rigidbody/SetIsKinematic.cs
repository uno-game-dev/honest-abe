//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of a Rigidbody's isKinematic property.
    /// </summary>
    [NodeInfo(  category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Sets the value of a Rigidbody\'s isKinematic property",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody-isKinematic.html")]
    public class SetIsKinematic : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new value of the isKinematic property. If Toggle, the value of isKinematic will be flipped.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The new value of the isKinematic property. If Toggle, the value of isKinematic will be flipped")]
        public BoolVar newIsKinematic;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newIsKinematic = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the rigidbody
            var rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (rigidbody == null)
                return Status.Error;

            // Set the isKinematic
            rigidbody.isKinematic = (newIsKinematic.isNone) ? !rigidbody.isKinematic : newIsKinematic.Value;

            return Status.Success;
        }
    }
}