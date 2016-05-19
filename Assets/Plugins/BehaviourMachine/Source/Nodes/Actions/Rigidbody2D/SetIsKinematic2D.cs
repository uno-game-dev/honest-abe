//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of a Rigidbody2D's isKinematic property.
    /// </summary>
    [NodeInfo(  category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Sets the value of a Rigidbody2D\'s isKinematic property",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D-isKinematic.html")]
    public class SetIsKinematic2D : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody2D in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody2D in it")]
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
            // Get the rigidbody2D
            var rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (rigidbody2D == null)
                return Status.Error;

            // Set the isKinematic
            rigidbody2D.isKinematic = (newIsKinematic.isNone) ? !rigidbody2D.isKinematic : newIsKinematic.Value;

            return Status.Success;
        }
    }
}
#endif